using AiConclave.Business.Application.Factions;
using AiConclave.Business.Domain.Entities;
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
    ///     Ensures that a faction is successfully created when all validations pass.
    /// </summary>
    [Fact]
    public async Task ShouldReturnSuccess_WhenAllValidationsPassed()
    {
        // Arrange
        var request = CreateFactionCommandBuilder.Build();

        FactionRepositoryMock
            .Setup(repo => repo.ExistsWithCodeAsync(request.Code))
            .ReturnsAsync(false);

        FactionRepositoryMock
            .Setup(repo => repo.ExistsWithNameAsync(request.Name))
            .ReturnsAsync(false);

        FactionRepositoryMock
            .Setup(repo => repo.AddAsync(It.IsAny<Faction>()))
            .ReturnsAsync((Faction faction) => faction);

        // Act
        await ExecuteUseCaseAsync(request);

        // Assert
        AssertNoErrors(request);
    }

    /// <summary>
    ///     Ensures that a faction is successfully created with custom resource amounts.
    /// </summary>
    [Fact]
    public async Task ShouldReturnSuccess_WhenCustomResourceAmountsAreProvided()
    {
        // Arrange
        var request = CreateFactionCommandBuilder
            .WithResource(Resource.Research, 15)
            .WithResource(Resource.Energy, 15)
            .WithResource(Resource.Materials, 10)
            .WithResource(Resource.Economy, 10)
            .WithResource(Resource.Stability, 5)
            .WithResource(Resource.Governance, 5)
            .WithResource(Resource.Co2, 0)
            .Build();

        FactionRepositoryMock
            .Setup(repo => repo.ExistsWithCodeAsync(request.Code))
            .ReturnsAsync(false);

        FactionRepositoryMock
            .Setup(repo => repo.ExistsWithNameAsync(request.Name))
            .ReturnsAsync(false);

        FactionRepositoryMock
            .Setup(repo => repo.AddAsync(It.IsAny<Faction>()))
            .ReturnsAsync((Faction faction) => faction);

        // Act
        await ExecuteUseCaseAsync(request);

        // Assert
        AssertNoErrors(request);
    }

    /// <summary>
    ///     Ensures that an error is returned when the faction code already exists.
    /// </summary>
    [Fact]
    public async Task ShouldReturnError_WhenFactionCodeAlreadyExists()
    {
        // Arrange
        var request = CreateFactionCommandBuilder.Build();

        FactionRepositoryMock
            .Setup(repo => repo.ExistsWithCodeAsync(request.Code))
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
            .Setup(repo => repo.ExistsWithCodeAsync(request.Code))
            .ReturnsAsync(false);

        FactionRepositoryMock
            .Setup(repo => repo.ExistsWithNameAsync(request.Name))
            .ReturnsAsync(true);

        // Act
        await ExecuteUseCaseAsync(request);

        // Assert
        AssertError($"A faction with name '{request.Name}' already exists.");
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
            .Setup(repo => repo.ExistsWithCodeAsync(request.Code))
            .ReturnsAsync(false);

        FactionRepositoryMock
            .Setup(repo => repo.ExistsWithNameAsync(request.Name))
            .ReturnsAsync(false);

        // Act
        await ExecuteUseCaseAsync(request);

        // Assert
        AssertError($"Invalid resource code: {invalidResourceCode}");
    }
}