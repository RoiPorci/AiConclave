using AiConclave.Business.Application;
using AiConclave.Business.Application.Factions;

namespace AiConclave.Business.Tests.ListFactionsTests.Helper;

/// <summary>
///     Builder class for creating instances of <see cref="ListFactionsQuery" />.
///     Provides methods to customize query properties for testing.
/// </summary>
public class ListFactionsQueryBuilder
{
    private readonly TestPresenter<ListFactionsResponse> _presenter;
    private string? _sortBy = ListFactionsSortOptions.Code;
    private string? _sortOrder = BaseSortOptions.Ascending;

    /// <summary>
    ///     Initializes a new instance of the <see cref="ListFactionsQueryBuilder" /> class.
    /// </summary>
    /// <param name="presenter">The test presenter to be used in the query.</param>
    public ListFactionsQueryBuilder(TestPresenter<ListFactionsResponse> presenter)
    {
        _presenter = presenter;
    }

    /// <summary>
    ///     Sets the field to sort by.
    /// </summary>
    /// <param name="sortBy">The field name to sort by (e.g., "name", "code").</param>
    /// <returns>The current <see cref="ListFactionsQueryBuilder" /> instance.</returns>
    public ListFactionsQueryBuilder WithSortBy(string sortBy)
    {
        _sortBy = sortBy;
        return this;
    }

    /// <summary>
    ///     Sets the sort order (e.g., "asc" or "desc").
    /// </summary>
    /// <param name="sortOrder">The sort order.</param>
    /// <returns>The current <see cref="ListFactionsQueryBuilder" /> instance.</returns>
    public ListFactionsQueryBuilder WithSortOrder(string sortOrder)
    {
        _sortOrder = sortOrder;
        return this;
    }

    /// <summary>
    ///     Builds a <see cref="ListFactionsQuery" /> instance with the configured values.
    /// </summary>
    /// <returns>A new <see cref="ListFactionsQuery" />.</returns>
    public ListFactionsQuery Build()
    {
        return new ListFactionsQuery(_sortBy, _sortOrder, _presenter);
    }
}
