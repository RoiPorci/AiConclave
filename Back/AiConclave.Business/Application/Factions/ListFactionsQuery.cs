namespace AiConclave.Business.Application.Factions;

public class ListFactionsQuery : IRequestWithPresenter<ListFactionsResponse>
{
    public string? SortBy { get; set; }
    
    public string? SortOrder { get; set; }
    
    public IResponsePresenter<ListFactionsResponse> Presenter { get; }
}