using System.Collections.Generic;
using System.Linq;
using AiConclave.Business.Domain.Entities;

namespace AiConclave.Business.Application.Factions.DTOs;

/// <summary>
/// Provides extension methods to map <see cref="OwnedResource"/> entities to <see cref="ResourceAmountDto"/> objects.
/// </summary>
public static class ResourceAmountDtoMapper
{
    /// <summary>
    /// Maps a dictionary of owned resources to a list of <see cref="ResourceAmountDto"/> instances.
    /// </summary>
    /// <param name="ownedResources">
    /// The dictionary of owned resources, where the key is the resource code and the value is the corresponding <see cref="OwnedResource"/>.
    /// </param>
    /// <returns>A list of <see cref="ResourceAmountDto"/> representing the resource amounts.</returns>
    public static List<ResourceAmountDto> MapToDtos(this Dictionary<string, OwnedResource> ownedResources)
    {
        return ownedResources
            .Select(kvp => new ResourceAmountDto(kvp.Key, kvp.Value.Amount))
            .ToList();
    }
}