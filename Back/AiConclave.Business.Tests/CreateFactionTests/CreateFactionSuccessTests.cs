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
    public async Task ShouldReturnSuccess_WhenAllValidtionsPassed()
    {
        // Arrange
        var request = new CreateFactionRequestBuilder().Build();
        
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
        await ExecuteRequestAsync(request);
        var response = Presenter.GetResponse();
        
        // Assert
        Assert.True(response.IsSuccess);
        Assert.NotNull(response.FactionId);
        Assert.Equal(request.Code, response.Code);
        Assert.Equal(request.Name, response.Name);
        Assert.Equal(request.Description, response.Description);
    }
}