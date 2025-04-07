using System;
using System.Collections.Generic;

namespace AiConclave.Business.Application.Factions.DTOs;

public class FactionDto
{
    /// <summary>
    ///     Gets or sets the unique identifier of the faction.
    /// </summary>
    public Guid? FactionId { get; set; }

    /// <summary>
    ///     Gets or sets the unique code of the faction.
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    ///     Gets or sets the name of the faction.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    ///     Gets or sets the description of the faction.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    ///     Gets or sets the list of resources for the faction.
    /// </summary>
    public List<ResourceAmountDto> OwnedResources { get; set; } = [];
}