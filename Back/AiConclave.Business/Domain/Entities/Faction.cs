using System;
using System.Collections.Generic;
using System.Linq;
using AiConclave.Business.Domain.Model;

namespace AiConclave.Business.Domain.Entities;

/// <summary>
/// Represents a faction with a unique identifier, name, description, and owned resources.
/// </summary>
public class Faction
{
    /// <summary>
    /// Gets or sets the unique identifier of the faction.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Gets or sets the code of the faction.
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Gets or sets the name of the faction.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the description of the faction.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets the dictionary of owned resources by the faction.
    /// The key is the resource code, and the value is the corresponding owned resource.
    /// A faction must always have all resources initialized with an amount of zero.
    /// </summary>
    public Dictionary<string, OwnedResource> OwnedResources { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Faction"/> class.
    /// </summary>
    /// <param name="id">The unique identifier of the faction.</param>
    /// <param name="code">The code of the faction.</param>
    /// <param name="name">The name of the faction.</param>
    /// <param name="description">The description of the faction.</param>
    private Faction(Guid id, string code, string name, string description)
    {
        Id = id;
        Code = code;
        Name = name;
        Description = description;
        InitializeResources();
    }

    /// <summary>
    /// Ensures that a faction always starts with all resources set to zero.
    /// </summary>
    private void InitializeResources()
    {
        const int initialAmount = 0;
        
        OwnedResources = Resource.All.ToDictionary(
            resource => resource.Code, 
            resource => OwnedResource.Create(initialAmount, this, resource.Code)
        );
    }

    /// <summary>
    /// Updates the amount of a specific resource owned by the faction.
    /// </summary>
    /// <param name="resourceCode">The code of the resource to update.</param>
    /// <param name="amount">The new amount of the resource.</param>
    /// <exception cref="ArgumentException">
    /// Thrown if the resource does not exist in the faction's owned resources.
    /// </exception>
    public void UpdateResourceAmount(string resourceCode, int amount)
    {
        if (!OwnedResources.TryGetValue(resourceCode, out var ownedResource))
            throw new ArgumentException($"Resource {resourceCode} does not exist for this faction.");

        ownedResource.Amount = amount;
    }

    /// <summary>
    /// Creates a new instance of <see cref="Faction"/> with a generated unique identifier.
    /// </summary>
    /// <param name="code">The code of the faction.</param>
    /// <param name="name">The name of the faction.</param>
    /// <param name="description">The description of the faction.</param>
    /// <returns>A new instance of <see cref="Faction"/>.</returns>
    public static Faction Create(string code, string name, string description)
    {
        return new Faction(Guid.NewGuid(), code, name, description);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Faction"/> class.
    /// This constructor is required for EF Core. It should never be used.
    /// </summary>
#pragma warning disable CS8618
    public Faction() {}  
#pragma warning restore CS8618
}
