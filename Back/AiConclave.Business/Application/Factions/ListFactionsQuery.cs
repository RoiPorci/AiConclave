namespace AiConclave.Business.Application.Factions;

/// <summary>
/// Query for retrieving a list of factions with optional sorting parameters.
/// </summary>
public class ListFactionsQuery : IRequestWithPresenter<ListFactionsResponse>
{
    /// <summary>
    /// Gets or sets the field by which to sort the factions (e.g., "Name", "Code").
    /// </summary>
    public string? SortBy { get; set; }

    /// <summary>
    /// Gets or sets the sort order ("asc" or "desc").
    /// </summary>
    public string? SortOrder { get; set; }

    /// <summary>
    /// Gets the presenter responsible for handling the response.
    /// </summary>
    public IResponsePresenter<ListFactionsResponse> Presenter { get; }
}