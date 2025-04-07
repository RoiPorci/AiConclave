using AiConclave.Business.Application;
using AiConclave.Business.Domain.Entities;
using AiConclave.Business.Tests.ListFactionsTests.Helper;
using Moq;

namespace AiConclave.Business.Tests.ListFactionsTests;

/// <summary>
/// Application-level tests for the ListFactions use case.
/// </summary>
public class ListFactionsApplicationTests : ListFactionsTestBase
{
    /// <summary>
    /// Ensures the use case falls back to default sorting (by code ascending)
    /// when invalid sort parameters are provided.
    /// </summary>
    [Fact]
    public async Task ShouldFallbackToDefaultSort_WhenSortParametersAreInvalid()
    {
        // Arrange
        var factions = new List<Faction>
        {
            Faction.Create("Z", "Zulu", "Desc Z"),
            Faction.Create("A", "Alpha", "Desc A")
        };

        // The handler will fallback to code + asc
        FactionRepositoryMock
            .Setup(repo => repo.GetWithResourcesAsync(SortOptions.DefaultSortBy, BaseSortOptions.DefaultSortOrder))
            .ReturnsAsync(factions.OrderBy(f => f.Code).ToList());

        var request = QueryBuilder
            .WithSortBy("invalid_field")
            .WithSortOrder("invalid_order")
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