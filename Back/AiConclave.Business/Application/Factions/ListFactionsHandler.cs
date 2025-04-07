using System.Threading;
using System.Threading.Tasks;
using AiConclave.Business.Application.Factions.DTOs;
using AiConclave.Business.Domain.Repositories;

namespace AiConclave.Business.Application.Factions;

/// <summary>
/// Handler for processing <see cref="ListFactionsQuery"/> requests and returning a list of factions.
/// </summary>
public class ListFactionsHandler : BaseHandler<ListFactionsQuery, ListFactionsResponse>
{
    private readonly ListFactionsSortOptions _sortOptions;
    private readonly IFactionRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="ListFactionsHandler"/> class.
    /// </summary>
    /// <param name="sortOptions">Options used to validate and apply sorting parameters.</param>
    /// <param name="repository">Repository for accessing faction data.</param>
    public ListFactionsHandler(ListFactionsSortOptions sortOptions, IFactionRepository repository)
    {
        _sortOptions = sortOptions;
        _repository = repository;
    }

    /// <summary>
    /// Handles the request to list factions with sorting.
    /// </summary>
    /// <param name="request">The request containing sort parameters.</param>
    /// <param name="cancellationToken">A token for cancelling the operation.</param>
    /// <returns>
    /// A <see cref="ListFactionsResponse"/> containing the sorted list of factions.
    /// </returns>
    protected override async Task<ListFactionsResponse> HandleRequest(ListFactionsQuery request, CancellationToken cancellationToken)
    {
        var response = new ListFactionsResponse();
        
        // 1. Validate sort or get default options
        var (sortBy, sortOrder) = _sortOptions.GetValidSortOptions(request.SortBy, request.SortOrder);
        
        // 2. Get the sorted factions
        var factions = await _repository.GetWithResourcesAsync(sortBy, sortOrder);
        
        // 3. Build and return response
        response.Factions = factions.MapToFactionDtos();
        return response;
    }
}
