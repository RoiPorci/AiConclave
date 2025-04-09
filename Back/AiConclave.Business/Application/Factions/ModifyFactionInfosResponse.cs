using System;

namespace AiConclave.Business.Application.Factions;

/// <summary>
/// Response model returned after successfully modifying a faction's information.
/// </summary>
public class ModifyFactionInfosResponse : BaseResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the modified faction.
    /// </summary>
    public Guid? FactionId { get; set; }
    
    /// <summary>
    /// Gets or sets the unique code of the faction.
    /// </summary>
    public string? Code { get; set; }
    
    /// <summary>
    /// Gets or sets the name of the modified faction.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the description of the modified faction.
    /// </summary>
    public string? Description { get; set; }
}