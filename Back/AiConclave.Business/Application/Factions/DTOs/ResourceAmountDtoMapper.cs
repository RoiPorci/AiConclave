using System.Collections.Generic;
using System.Linq;
using AiConclave.Business.Domain.Entities;

namespace AiConclave.Business.Application.Factions.DTOs;

public static class ResourceAmountDtoMapper
{
    public static List<ResourceAmountDto> MapToDtos(this Dictionary<string, OwnedResource> ownedResources)
    {
        return ownedResources
            .Select(kvp => new ResourceAmountDto(kvp.Key, kvp.Value.Amount))
            .ToList();
    }
}