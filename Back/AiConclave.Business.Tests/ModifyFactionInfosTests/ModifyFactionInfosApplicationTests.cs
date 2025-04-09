using AiConclave.Business.Domain.Entities;
using AiConclave.Business.Domain.Model;
using AiConclave.Business.Tests.ModifyFactionInfosTests.Helpers;
using Moq;

namespace AiConclave.Business.Tests.ModifyFactionInfosTests;

/// <summary>
/// Application-level tests for the ModifyFactionInfos use case.
/// Validates integration between handler, repository, and validation logic.
/// </summary>
public class ModifyFactionInfosApplicationTests : ModifyFactionInfosTestBase
{
    /// <summary>
    /// Ensures that the use case returns an error when the specified faction does not exist.
    /// </summary>
    [Fact]
    public async Task ShouldReturnError_WhenFactionNotFound()
    {
        // Arrange
        var request = ModifyFactionInfosCommandBuilder.Build();
        
        FactionRepositoryMock
            .Setup(repo => repo.GetByIdWithResourcesAsync(request.FactionId))
            .ReturnsAsync((Faction?)null);

        // Act
        await ExecuteUseCaseAsync(request);

        // Assert
        AssertError($"Faction with ID {request.FactionId} not found.");
    }
    
    /// <summary>
    /// Ensures that an error is returned when the provided faction code already exists in another faction.
    /// </summary>
    [Fact]
    public async Task ShouldReturnError_WhenFactionCodeAlreadyExists()
    {
        // Arrange
        var request = ModifyFactionInfosCommandBuilder.Build();
        var existingFaction = Faction.Create(request.Code!, "TestOld", "TestOld");
        foreach (var resource in Resource.All)
        {
            var amount = resource.Code == Resource.Co2.Code ? 0 : 10;
            existingFaction.UpdateResourceAmount(resource.Code, amount);
        }

        FactionRepositoryMock
            .Setup(repo => repo.GetByIdWithResourcesAsync(request.FactionId))
            .ReturnsAsync(existingFaction);

        FactionRepositoryMock
            .Setup(repo => repo.ExistsWithCodeAsync(request.Code!, It.IsAny<Guid>()))
            .ReturnsAsync(true);

        // Act
        await ExecuteUseCaseAsync(request);

        // Assert
        AssertError($"A faction with code '{request.Code}' already exists.");
    }

    /// <summary>
    /// Ensures that an error is returned when the provided faction name already exists in another faction.
    /// </summary>
    [Fact]
    public async Task ShouldReturnError_WhenFactionNameAlreadyExists()
    {
        // Arrange
        var request = ModifyFactionInfosCommandBuilder.Build();
        var existingFaction = Faction.Create("OLD", request.Name!, "TestOld");
        foreach (var resource in Resource.All)
        {
            var amount = resource.Code == Resource.Co2.Code ? 0 : 10;
            existingFaction.UpdateResourceAmount(resource.Code, amount);
        }

        FactionRepositoryMock
            .Setup(repo => repo.GetByIdWithResourcesAsync(request.FactionId))
            .ReturnsAsync(existingFaction);

        FactionRepositoryMock
            .Setup(repo => repo.ExistsWithNameAsync(request.Name!, It.IsAny<Guid>()))
            .ReturnsAsync(true);

        // Act
        await ExecuteUseCaseAsync(request);

        // Assert
        AssertError($"A faction with name '{request.Name}' already exists.");
    }
}