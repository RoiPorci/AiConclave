using AiConclave.Business.Domain.Entities;
using AiConclave.Business.Tests.CreateFactionTests.Helpers;
using Moq;

namespace AiConclave.Business.Tests.CreateFactionTests;

/// <summary>
/// Tests for successful faction creation when all validations pass.
/// </summary>
public class CreateFactionSuccessTests : CreateFactionTestBase
{
    /// <summary>
    /// Ensures that a faction is successfully created when all validation checks pass.
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

        Faction createdFaction = null!;
        FactionRepositoryMock
            .Setup(repo => repo.AddAsync(It.IsAny<Faction>()))
            .ReturnsAsync((Faction faction) =>
            {
                createdFaction = faction;
                return faction;
            });

        // Act
        await ExecuteUseCaseAsync(request);

        // Assert
        AssertNoErrors(request);
        AssertResourcesInitializedToZero(createdFaction);
    }
}