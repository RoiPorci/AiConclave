using System;

namespace AiConclave.Business.Application.Factions;

/// <summary>
///     Response model for the faction creation use case.
/// </summary>
public class CreateFactionResponse : BaseResponse
{
    /// <summary>
    ///     Gets or sets the unique identifier of the created faction.
    /// </summary>
    public Guid? FactionId { get; set; }

    /// <summary>
    ///     Gets or sets the unique code of the faction.
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    ///     Gets or sets the name of the created faction.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    ///     Gets or sets the description of the created faction.
    /// </summary>
    public string? Description { get; set; }
}