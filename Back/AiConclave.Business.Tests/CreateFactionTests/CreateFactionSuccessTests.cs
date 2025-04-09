using AiConclave.Business.Domain.Entities;
using AiConclave.Business.Domain.Model;
using AiConclave.Business.Tests.CreateFactionTests.Helpers;
using Moq;

namespace AiConclave.Business.Tests.CreateFactionTests;

/// <summary>
///     Tests for successful faction creation when all validations pass.
/// </summary>
public class CreateFactionSuccessTests : CreateFactionTestBase
{
    /// <summary>
    ///     Ensures that a faction is successfully created when all validation checks pass.
    /// </summary>
    [Fact]
    public async Task ShouldReturnSuccess_WhenAllValidationsPassed()
    {
        // Arrange
        var request = CreateFactionCommandBuilder.Build();

        FactionRepositoryMock
            .Setup(repo => repo.ExistsWithCodeAsync(request.Code, It.IsAny<Guid>()))
            .ReturnsAsync(false);

        FactionRepositoryMock
            .Setup(repo => repo.ExistsWithNameAsync(request.Name, It.IsAny<Guid>()))
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
            .Setup(repo => repo.ExistsWithCodeAsync(request.Code, It.IsAny<Guid>()))
            .ReturnsAsync(false);

        FactionRepositoryMock
            .Setup(repo => repo.ExistsWithNameAsync(request.Name, It.IsAny<Guid>()))
            .ReturnsAsync(false);

        FactionRepositoryMock
            .Setup(repo => repo.AddAsync(It.IsAny<Faction>()))
            .ReturnsAsync((Faction faction) => faction);

        // Act
        await ExecuteUseCaseAsync(request);

        // Assert
        AssertNoErrors(request);
    }
}