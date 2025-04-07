using System.Collections.Generic;
using System.Linq;
using AiConclave.Business.Domain.Entities;

namespace AiConclave.Business.Application.Factions.DTOs;

public static class FactionDtoMapper
{
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