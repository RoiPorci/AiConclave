using AiConclave.Business.Application.Factions.OwnedResources;
using AiConclave.Business.Domain.Entities;
using AiConclave.Business.Domain.Model;
using AiConclave.Business.Tests.InitFactionResourcesTests.Helpers;
using Moq;

namespace AiConclave.Business.Tests.InitFactionResourcesTests;

/// <summary>
/// Application-level tests for the SetFactionInitialResources use case.
/// </summary>
public class InitFactionResourcesApplicationTests : InitFactionResourcesTestBase
{
    /// <summary>
    /// Verifies that an error is returned when the faction does not exist.
    /// </summary>
    [Fact]
    public async Task ShouldReturnError_WhenFactionDoesNotExist()
    {
        var command = CommandBuilder.Build();

        FactionRepositoryMock
            .Setup(r => r.GetByIdAsync(command.FactionId))
            .ReturnsAsync((Faction?)null);

        await ExecuteUseCaseAsync(command);

        AssertError($"Faction with ID {command.FactionId} not found.");
    }

    /// <summary>
    /// Verifies that an error is returned when an invalid resource code is included in the request.
    /// </summary>
    [Fact]
    public async Task ShouldReturnError_WhenInvalidResourceCodeIsProvided()
    {
        const string invalidResourceCode = "INVALID_CODE";
        
        var factionId = Guid.NewGuid();
        var command = CommandBuilder
            .WithFactionId(factionId)
            .WithResourceAmounts([
                new ResourceAmountDto(Resource.Research.Code, 10),
                new ResourceAmountDto(Resource.Energy.Code, 10),
                new ResourceAmountDto(Resource.Materials.Code, 10),
                new ResourceAmountDto(Resource.Economy.Code, 10),
                new ResourceAmountDto(Resource.Stability.Code, 10),
                new ResourceAmountDto(Resource.Governance.Code, 5),
                new ResourceAmountDto(invalidResourceCode, 5)
            ])
            .Build();

        var faction = CreateFaction(factionId, "VLD", "Valid", "Valid Faction");

        FactionRepositoryMock
            .Setup(r => r.GetByIdAsync(factionId))
            .ReturnsAsync(faction);

        await ExecuteUseCaseAsync(command);

        AssertError($"Invalid resource code: {invalidResourceCode}");
    }

    /// <summary>
    /// Verifies that an error is returned when the initial CO2 resource is negative.
    /// </summary>
    [Fact]
    public async Task ShouldReturnError_WhenCO2IsNegative()
    {
        var factionId = Guid.NewGuid();
        var command = CommandBuilder
            .WithFactionId(factionId)
            .WithResource(Resource.Co2, -5)         // invalid
            .WithResource(Resource.Governance, 15)  // adjust to stay at 60
            .Build();

        var faction = CreateFaction(factionId, "NEG", "Negative Co2", "Oops");

        FactionRepositoryMock
            .Setup(r => r.GetByIdAsync(factionId))
            .ReturnsAsync(faction);

        await ExecuteUseCaseAsync(command);

        AssertError("Co2 cannot be negative at initialization.");
    }
}
