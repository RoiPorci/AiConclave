using System;
using System.Collections.Generic;

namespace AiConclave.Business.Application.Factions.OwnedResources;

/// <summary>
/// Command used to set the initial resources of a specific <see cref="Faction"/>.
/// </summary>
public class InitFactionResourcesCommand : IRequestWithPresenter<InitFactionResourcesResponse>
{
    /// <summary>
    /// Gets or sets the unique identifier of the faction to update.
    /// </summary>
    public Guid FactionId { get; set; }

    /// <summary>
    /// Gets or sets the list of resource amounts to assign to the faction.
    /// </summary>
    public List<ResourceAmountDto> ResourceAmounts { get; set; } = new();

    /// <summary>
    /// Gets or sets the presenter responsible for handling the response.
    /// </summary>
    public IResponsePresenter<InitFactionResourcesResponse> Presenter { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="InitFactionResourcesCommand"/> class.
    /// </summary>
    /// <param name="factionId">The unique identifier of the faction.</param>
    /// <param name="resourceAmounts">The list of resource amounts to set.</param>
    /// <param name="presenter">The presenter that will handle the response.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="presenter"/> is <see langword="null"/>.</exception>
    public InitFactionResourcesCommand
    (
        Guid factionId, 
        List<ResourceAmountDto> resourceAmounts, 
        IResponsePresenter<InitFactionResourcesResponse> presenter
    )
    {
        FactionId = factionId;
        ResourceAmounts = resourceAmounts;
        Presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
    }
}