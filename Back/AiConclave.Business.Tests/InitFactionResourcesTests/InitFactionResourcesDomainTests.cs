using AiConclave.Business.Application.Factions.OwnedResources;
using AiConclave.Business.Domain.Model;
using AiConclave.Business.Tests.InitFactionResourcesTests.Helpers;
using Moq;

namespace AiConclave.Business.Tests.InitFactionResourcesTests;

/// <summary>
///     Domain-level tests for validating business rules related to faction resource initialization.
/// </summary>
public class InitFactionResourcesDomainTests : InitFactionResourcesTestBase
{
    /// <summary>
    ///     Ensures that an error is returned when the total resource amount does not match the expected total.
    /// </summary>
    [Fact]
    public async Task ShouldReturnError_WhenTotalResourceAmountIsIncorrect()
    {
        var command = CommandBuilder
            .WithResource(Resource.Co2, 4) // instead of 0
            .Build();

        var faction = CreateFaction(command.FactionId, "ABC", "Test", "Test Desc");

        FactionRepositoryMock
            .Setup(r => r.GetByIdAsync(command.FactionId))
            .ReturnsAsync(faction);

        await ExecuteUseCaseAsync(command);

        var total = command.ResourceAmounts.Sum(r => r.Amount);
        AssertError(
            $"The total amount of resources must equal {InitFactionResourcesRuleChecker.ExpectedInitialTotal}, but is {total}.");
    }

    /// <summary>
    ///     Ensures that an error is returned when a resource that does not allow negative values is assigned a negative
    ///     amount.
    /// </summary>
    [Fact]
    public async Task ShouldReturnError_WhenNegativeValueIsUsedOnNonNegativeResource()
    {
        var command = CommandBuilder
            .WithResource(Resource.Research, -10)
            .WithResource(Resource.Co2, 20) // to keep the total equal to 60
            .Build();

        var faction = CreateFaction(command.FactionId, "XYZ", "Test3", "Negative");

        FactionRepositoryMock
            .Setup(r => r.GetByIdAsync(command.FactionId))
            .ReturnsAsync(faction);

        await ExecuteUseCaseAsync(command);

        AssertError("Resource 'Research & Development' cannot have a negative amount.");
    }
}