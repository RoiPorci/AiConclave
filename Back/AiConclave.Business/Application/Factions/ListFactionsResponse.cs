using System.Collections.Generic;
using AiConclave.Business.Application.Factions.DTOs;

namespace AiConclave.Business.Application.Factions;

/// <summary>
///     Response model containing a list of factions, returned by the <see cref="ListFactionsHandler"/>.
/// </summary>
public class ListFactionsResponse : BaseResponse
{
    /// <summary>
    ///     Gets or sets the list of factions returned by the query.
    /// </summary>
    public List<FactionDto> Factions { get; set; } = [];
}