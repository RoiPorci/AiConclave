using System.Threading;
using System.Threading.Tasks;
using AiConclave.Business.Application.Factions.DTOs;
using AiConclave.Business.Domain.Repositories;

namespace AiConclave.Business.Application.Factions;

public class ListFactionsHandler : BaseHandler<ListFactionsQuery, ListFactionsResponse>
{
    private readonly ListFactionsSortOptions _sortOptions;
    private readonly IFactionRepository _repository;

    public ListFactionsHandler(ListFactionsSortOptions sortOptions, IFactionRepository repository)
    {
        _sortOptions = sortOptions;
        _repository = repository;
    }

    protected override async Task<ListFactionsResponse> HandleRequest(ListFactionsQuery request, CancellationToken cancellationToken)
    {
        var response = new ListFactionsResponse();
        
        // 1. Validate sort or get default options
        var (sortBy, sortOrder) = _sortOptions.GetValidSortOptions(request.SortBy, request.SortOrder);
        
        //2. Get the sorted factions
        var factions = await _repository.GetWithResourcesAsync(sortBy, sortOrder);
        
        //3. Build and return response
        response.Factions = factions.MapToFactionDtos();
        return response;
    }
}