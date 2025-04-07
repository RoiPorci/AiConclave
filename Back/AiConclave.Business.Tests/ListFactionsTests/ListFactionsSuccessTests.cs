using AiConclave.Business.Application;
using AiConclave.Business.Application.Factions;
using AiConclave.Business.Domain.Entities;
using AiConclave.Business.Tests.ListFactionsTests.Helper;
using Moq;

namespace AiConclave.Business.Tests.ListFactionsTests;

/// <summary>
/// Tests for successful execution of the ListFactions use case with valid input.
/// </summary>
public class ListFactionsSuccessTests : ListFactionsTestBase
{
    /// <summary>
    /// Ensures that factions are correctly sorted and returned when valid sort parameters are provided.
    /// </summary>
    [Fact]
    public async Task ShouldReturnSortedFactions_WhenValidSortParametersProvided()
    {
        // Arrange
        var factions = new List<Faction>
        {
            Faction.Create("B", "Beta", "Desc B"),
            Faction.Create("A", "Alpha", "Desc A")
        };

        FactionRepositoryMock
            .Setup(repo => repo.GetWithResourcesAsync(ListFactionsSortOptions.Code, BaseSortOptions.Ascending))
            .ReturnsAsync(factions.OrderBy(f => f.Code).ToList());

        var request = QueryBuilder
            .WithSortBy(ListFactionsSortOptions.Code)
            .WithSortOrder(BaseSortOptions.Ascending)
            .Build();

        // Act
        await ExecuteUseCaseAsync(request);

        // Assert
        var response = Presenter.GetResponse();
        Assert.True(response.IsSuccess);
        Assert.Equal(factions.Count, response.Factions.Count);
        Assert.Equal(factions[1].Code, response.Factions[0].Code);
        Assert.Equal(factions[0].Code, response.Factions[1].Code);
    }
}