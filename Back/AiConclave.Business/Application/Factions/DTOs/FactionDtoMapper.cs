using System.Collections.Generic;
using System.Linq;
using AiConclave.Business.Domain.Entities;

namespace AiConclave.Business.Application.Factions.DTOs;

/// <summary>
/// Provides extension methods to map <see cref="Faction"/> entities to <see cref="FactionDto"/> instances.
/// </summary>
public static class FactionDtoMapper
{
    /// <summary>
    /// Maps a list of <see cref="Faction"/> entities to a list of <see cref="FactionDto"/> DTOs.
    /// </summary>
    /// <param name="factions">The list of factions to map.</param>
    /// <returns>A list of <see cref="FactionDto"/> objects.</returns>
    public static List<FactionDto> MapToFactionDtos(this List<Faction> factions)
    {
        return factions.Select(faction => new FactionDto
        {
            Code = faction.Code,
            Name = faction.Name,
            Description = faction.Description,
            OwnedResources = faction.OwnedResources.MapToDtos()
        }).ToList();
    }
}