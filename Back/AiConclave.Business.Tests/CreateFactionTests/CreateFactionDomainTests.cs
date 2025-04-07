using AiConclave.Business.Application.Factions;
using AiConclave.Business.Domain.Model;
using AiConclave.Business.Tests.CreateFactionTests.Helpers;
using Moq;

namespace AiConclave.Business.Tests.CreateFactionTests;

/// <summary>
///     Domain-level tests for validating business rules related to faction creation and resource initialization.
/// </summary>
public class CreateFactionDomainTests : CreateFactionTestBase
{
    /// <summary>
    ///     Ensures that an error is returned when the total resource amount is incorrect.
    /// </summary>
    [Fact]
    public async Task ShouldReturnError_WhenTotalResourceAmountIsIncorrect()
    {
        // Arrange
        var request = CreateFactionCommandBuilder
            .WithResource(Resource.Co2, 4) // instead of 0
            .Build();

        FactionRepositoryMock
            .Setup(repo => repo.ExistsWithCodeAsync(request.Code))
            .ReturnsAsync(false);

        FactionRepositoryMock
            .Setup(repo => repo.ExistsWithNameAsync(request.Name))
            .ReturnsAsync(false);

        // Act
        await ExecuteUseCaseAsync(request);

        // Assert
        var total = request.ResourceAmounts.Sum(r => r.Amount);
        AssertError(
            $"The total amount of resources must equal {CreateFactionRuleChecker.ExpectedInitResourcesTotalAmount}, but is {total}.");
    }

    /// <summary>
    ///     Ensures that an error is returned when a negative value is used for a non-negative resource.
    /// </summary>
    [Fact]
    public async Task ShouldReturnError_WhenNegativeValueIsUsedOnNonNegativeResource()
    {
        // Arrange
        var request = CreateFactionCommandBuilder
            .WithResource(Resource.Research, -10)
            .WithResource(Resource.Co2, 20) // to keep the total equal to 60
            .Build();

        FactionRepositoryMock
            .Setup(repo => repo.ExistsWithCodeAsync(request.Code))
            .ReturnsAsync(false);

        FactionRepositoryMock
            .Setup(repo => repo.ExistsWithNameAsync(request.Name))
            .ReturnsAsync(false);

        // Act
        await ExecuteUseCaseAsync(request);

        // Assert
        AssertError($"Resource '{Resource.Research.Name}' cannot have a negative amount.");
    }

    /// <summary>
    ///     Ensures that an error is returned when the CO2 resource is negative.
    /// </summary>
    [Fact]
    public async Task ShouldReturnError_WhenCO2IsNegative()
    {
        // Arrange
        var request = CreateFactionCommandBuilder
            .WithResource(Resource.Co2, -5) // invalid
            .WithResource(Resource.Governance, 15) // adjust to stay at 60
            .Build();

        FactionRepositoryMock
            .Setup(repo => repo.ExistsWithCodeAsync(request.Code))
            .ReturnsAsync(false);

        FactionRepositoryMock
            .Setup(repo => repo.ExistsWithNameAsync(request.Name))
            .ReturnsAsync(false);

        // Act
        await ExecuteUseCaseAsync(request);

        // Assert
        AssertError("Co2 cannot be negative at initialization.");
    }

    /// <summary>
    ///     Ensures that an error is returned when the faction code is invalid.
    /// </summary>
    [Fact]
    public async Task ShouldReturnError_WhenFactionCodeIsInvalid()
    {
        // Arrange
        var request = CreateFactionCommandBuilder
            .WithCode("") // Invalid empty code
            .Build();

        FactionRepositoryMock
            .Setup(repo => repo.ExistsWithCodeAsync(request.Code))
            .ReturnsAsync(false);

        FactionRepositoryMock
            .Setup(repo => repo.ExistsWithNameAsync(request.Name))
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
        var request = CreateFactionCommandBuilder
            .WithName("") // Invalid empty name
            .Build();

        FactionRepositoryMock
            .Setup(repo => repo.ExistsWithCodeAsync(request.Code))
            .ReturnsAsync(false);

        FactionRepositoryMock
            .Setup(repo => repo.ExistsWithNameAsync(request.Name))
            .ReturnsAsync(false);

        // Act
        await ExecuteUseCaseAsync(request);

        // Assert
        AssertError("Faction name is required.");
    }
}