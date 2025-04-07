using AiConclave.Business.Application.Factions;
using AiConclave.Business.Application.Factions.DTOs;
using AiConclave.Business.Domain.Repositories;
using Moq;

namespace AiConclave.Business.Tests.ListFactionsTests.Helper;

/// <summary>
/// Base class for testing the ListFactions use case.
/// Provides shared setup, mocks, builders, and helpers for derived test classes.
/// </summary>
public abstract class ListFactionsTestBase
{
    /// <summary>
    /// Mocked repository for retrieving faction data.
    /// </summary>
    protected readonly Mock<IFactionRepository> FactionRepositoryMock = new();

    /// <summary>
    /// Presenter used to capture and inspect the use case response.
    /// </summary>
    protected readonly TestPresenter<ListFactionsResponse> Presenter = new();
    
    protected readonly ListFactionsSortOptions  SortOptions = new();

    private readonly ListFactionsHandler _handler;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="ListFactionsTestBase"/> class.
    /// Sets up the use case handler with mock dependencies.
    /// </summary>
    protected ListFactionsTestBase()
    {
        _handler = new ListFactionsHandler(SortOptions, FactionRepositoryMock.Object);
    }

    /// <summary>
    /// Gets a builder for constructing <see cref="ListFactionsQuery"/> instances.
    /// </summary>
    protected ListFactionsQueryBuilder QueryBuilder => new(Presenter);

    /// <summary>
    /// Executes the use case with the given query.
    /// </summary>
    /// <param name="query">The query to execute.</param>
    protected async Task ExecuteUseCaseAsync(ListFactionsQuery query)
    {
        await _handler.Handle(query, CancellationToken.None);
    }

    /// <summary>
    /// Extracts the list of factions from the use case response.
    /// </summary>
    /// <returns>A list of <see cref="FactionDto"/> objects from the response.</returns>
    protected List<FactionDto> GetFactionsFromResponse()
    {
        var response = Presenter.GetResponse();
        Assert.True(response.IsSuccess);
        return response.Factions;
    }
}
