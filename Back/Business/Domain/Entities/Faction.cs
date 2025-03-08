using System;
using System.Collections.Generic;

namespace Business.Domain.Entities;

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
    /// Gets or sets the name of the faction.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the description of the faction.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the collection of resources owned by the faction.
    /// </summary>
    public ICollection<OwnedResource> OwnedResources { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Faction"/> class.
    /// </summary>
    /// <param name="id">The unique identifier of the faction.</param>
    /// <param name="name">The name of the faction.</param>
    /// <param name="description">The description of the faction.</param>
    private Faction(Guid id, string name, string description)
    {
        Id = id;
        Name = name;
        Description = description;
    }

    /// <summary>
    /// Creates a new instance of <see cref="Faction"/> with a generated unique identifier.
    /// </summary>
    /// <param name="name">The name of the faction.</param>
    /// <param name="description">The description of the faction.</param>
    /// <returns>A new instance of <see cref="Faction"/>.</returns>
    public static Faction Create(string name, string description)
    {
        return new Faction(Guid.NewGuid(), name, description);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Faction"/> class.
    /// This constructor is required for EF Core. It should never be used.
    /// </summary>
#pragma warning disable CS8618
    public Faction() {}  
#pragma warning restore CS8618
}

