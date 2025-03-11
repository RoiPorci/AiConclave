using Business.Application;

namespace AiConclave.Business.Application.Factions;

/// <summary>
/// Request model for creating a new faction.
/// </summary>
public class CreateFactionRequest : IUseCaseRequest
{
    /// <summary>
    /// Gets or sets the unique code of the faction.
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Gets or sets the name of the faction.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the description of the faction.
    /// </summary>
    public string Description { get; set; }
}