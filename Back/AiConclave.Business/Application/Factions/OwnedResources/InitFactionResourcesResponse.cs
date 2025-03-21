using System;
using System.Collections.Generic;

namespace AiConclave.Business.Application.Factions.OwnedResources;

/// <summary>
/// Response returned after successfully setting the initial resources of a <see cref="Faction"/>.
/// </summary>
public class InitFactionResourcesResponse : BaseResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the updated faction.
    /// </summary>
    public Guid FactionId { get; set; }

    /// <summary>
    /// Gets or sets the list of updated resources for the faction.
    /// </summary>
    public List<ResourceAmountDto> UpdatedResources { get; set; } = new();
}