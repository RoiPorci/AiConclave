using AiConclave.Business.Application.Factions;
using AiConclave.Business.Application.Factions.DTOs;
using AiConclave.Business.Domain.Model;
using AiConclave.Business.Tests.CreateFactionTests.Helpers;
using Moq;

namespace AiConclave.Business.Tests.CreateFactionTests;

/// <summary>
///     Application-level tests for the CreateFaction use case.
/// </summary>
public class CreateFactionApplicationTests : CreateFactionTestBase
{
    /// <summary>
    ///     Ensures that an error is returned when the faction code already exists.
    /// </summary>
    [Fact]
    public async Task ShouldReturnError_WhenFactionCodeAlreadyExists()
    {
        // Arrange
        var request = CreateFactionCommandBuilder.Build();

        FactionRepositoryMock
            .Setup(repo => repo.ExistsWithCodeAsync(request.Code, It.IsAny<Guid>()))
            .ReturnsAsync(true);

        // Act
        await ExecuteUseCaseAsync(request);

        // Assert
        AssertError($"A faction with code '{request.Code}' already exists.");
    }

    /// <summary>
    ///     Ensures that an error is returned when the faction name already exists.
    /// </summary>
    [Fact]
    public async Task ShouldReturnError_WhenFactionNameAlreadyExists()
    {
        // Arrange
        var request = CreateFactionCommandBuilder.Build();

        FactionRepositoryMock
            .Setup(repo => repo.ExistsWithCodeAsync(request.Code, It.IsAny<Guid>()))
            .ReturnsAsync(false);

        FactionRepositoryMock
            .Setup(repo => repo.ExistsWithNameAsync(request.Name, It.IsAny<Guid>()))
            .ReturnsAsync(true);

        // Act
        await ExecuteUseCaseAsync(request);

        // Assert
        AssertError($"A faction with name '{request.Name}' already exists.");
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
            .Setup(repo => repo.ExistsWithCodeAsync(request.Code, It.IsAny<Guid>()))
            .ReturnsAsync(false);

        FactionRepositoryMock
            .Setup(repo => repo.ExistsWithNameAsync(request.Name, It.IsAny<Guid>()))
            .ReturnsAsync(false);

        // Act
        await ExecuteUseCaseAsync(request);

        // Assert
        AssertError("Co2 cannot be negative at initialization.");
    }
    
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
            .Setup(repo => repo.ExistsWithCodeAsync(request.Code, It.IsAny<Guid>()))
            .ReturnsAsync(false);

        FactionRepositoryMock
            .Setup(repo => repo.ExistsWithNameAsync(request.Name, It.IsAny<Guid>()))
            .ReturnsAsync(false);

        // Act
        await ExecuteUseCaseAsync(request);

        // Assert
        var total = request.ResourceAmounts.Sum(r => r.Amount);
        AssertError(
            $"The total amount of resources must equal {CreateFactionRuleChecker.ExpectedInitResourcesTotalAmount}, but is {total}.");
    }

    /// <summary>
    ///     Ensures that an error is returned when an invalid resource code is provided.
    /// </summary>
    [Fact]
    public async Task ShouldReturnError_WhenInvalidResourceCodeIsProvided()
    {
        // Arrange
        const string invalidResourceCode = "INVALID_CODE";
        var request = CreateFactionCommandBuilder
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

        FactionRepositoryMock
            .Setup(repo => repo.ExistsWithCodeAsync(request.Code, It.IsAny<Guid>()))
            .ReturnsAsync(false);

        FactionRepositoryMock
            .Setup(repo => repo.ExistsWithNameAsync(request.Name, It.IsAny<Guid>()))
            .ReturnsAsync(false);

        // Act
        await ExecuteUseCaseAsync(request);

        // Assert
        AssertError($"Invalid resource code: {invalidResourceCode}");
    }
}