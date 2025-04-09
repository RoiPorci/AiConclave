using AiConclave.Business.Domain.Entities;
using AiConclave.Business.Domain.Model;
using AiConclave.Business.Tests.ModifyFactionInfosTests.Helpers;
using Moq;

namespace AiConclave.Business.Tests.ModifyFactionInfosTests;

/// <summary>
/// Tests for successful execution of the ModifyFactionInfos use case.
/// </summary>
public class ModifyFactionInfosSuccessTests : ModifyFactionInfosTestBase
{
    /// <summary>
    /// Ensures the use case returns success when the faction exists and passes all validations.
    /// </summary>
    [Fact]
    public async Task ShouldReturnSuccess_WhenFactionUpdatedSuccessfully()
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
            .Build();

        FactionRepositoryMock
            .Setup(repo => repo.GetByIdWithResourcesAsync(request.FactionId))
            .ReturnsAsync(existingFaction);
        
        FactionRepositoryMock
            .Setup(repo => repo.ExistsWithCodeAsync(request.Code!, It.IsAny<Guid>()))
            .ReturnsAsync(false);

        FactionRepositoryMock
            .Setup(repo => repo.ExistsWithNameAsync(request.Name!, It.IsAny<Guid>()))
            .ReturnsAsync(false);

        FactionRepositoryMock
            .Setup(repo => repo.UpdateAsync(It.IsAny<Faction>()))
            .Returns(Task.CompletedTask);

        // Act
        await ExecuteUseCaseAsync(request);

        // Assert
        AssertNoErrors(request);
    }
}