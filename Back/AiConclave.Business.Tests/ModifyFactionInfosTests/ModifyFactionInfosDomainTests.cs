using AiConclave.Business.Domain.Entities;
using AiConclave.Business.Domain.Model;
using AiConclave.Business.Tests.ModifyFactionInfosTests.Helpers;
using Moq;

namespace AiConclave.Business.Tests.ModifyFactionInfosTests;

/// <summary>
/// Domain-level tests for the ModifyFactionInfos use case.
/// Verifies business rules such as uniqueness of faction code and name.
/// </summary>
public class ModifyFactionInfosDomainTests : ModifyFactionInfosTestBase
{
    /// <summary>
    ///     Ensures that an error is returned when the faction code is invalid.
    /// </summary>
    [Fact]
    public async Task ShouldReturnError_WhenFactionCodeIsInvalid()
    {
        // Arrange
        var existingFaction = Faction.Create("OLD", "TestOld", "TestOld");
        foreach (var resource in Resource.All)
        {
            var amount = resource.Code == Resource.Co2.Code ? 0 : 10;
            existingFaction.UpdateResourceAmount(resource.Code, amount);
        }
        
        var request = ModifyFactionInfosCommandBuilder
            .WithFactionId(existingFaction.Id)
            .WithCode("") // Invalid empty code
            .Build();
        
        FactionRepositoryMock
            .Setup(repo => repo.GetByIdWithResourcesAsync(existingFaction.Id))
            .ReturnsAsync(existingFaction);

        FactionRepositoryMock
            .Setup(repo => repo.ExistsWithCodeAsync(request.Code!, It.IsAny<Guid>()))
            .ReturnsAsync(false);

        FactionRepositoryMock
            .Setup(repo => repo.ExistsWithNameAsync(request.Name!, It.IsAny<Guid>()))
            .ReturnsAsync(false);

        // Act
        await ExecuteUseCaseAsync(request);

        // Assert
        AssertError("Faction code is required.");
    }

    /// <summary>
    ///     Ensures that an error is returned when the faction name is invalid.
    /// </summary>
    [Fact]
    public async Task ShouldReturnError_WhenFactionNameIsInvalid()
    {
        // Arrange
        var existingFaction = Faction.Create("OLD", "TestOld", "TestOld");
        foreach (var resource in Resource.All)
        {
            var amount = resource.Code == Resource.Co2.Code ? 0 : 10;
            existingFaction.UpdateResourceAmount(resource.Code, amount);
        }
        
        FactionRepositoryMock
            .Setup(repo => repo.GetByIdWithResourcesAsync(existingFaction.Id))
            .ReturnsAsync(existingFaction);
        
        var request = ModifyFactionInfosCommandBuilder
            .WithFactionId(existingFaction.Id)
            .WithName("") // Invalid empty name
            .Build();

        FactionRepositoryMock
            .Setup(repo => repo.ExistsWithCodeAsync(request.Code!, It.IsAny<Guid>()))
            .ReturnsAsync(false);

        FactionRepositoryMock
            .Setup(repo => repo.ExistsWithNameAsync(request.Name!, It.IsAny<Guid>()))
            .ReturnsAsync(false);

        // Act
        await ExecuteUseCaseAsync(request);

        // Assert
        AssertError("Faction name is required.");
    }
}