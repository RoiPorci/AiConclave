using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AiConclave.Business.Domain.Entities;

namespace AiConclave.Business.Domain.Repositories;

/// <summary>
///     Repository interface for managing <see cref="Faction" /> entities.
/// </summary>
public interface IFactionRepository
{
    /// <summary>
    ///     Adds a new faction asynchronously.
    /// </summary>
    /// <param name="faction">The faction to add.</param>
    /// <returns>
    ///     A task representing the asynchronous operation, containing the added <see cref="Faction" />.
    /// </returns>
    Task<Faction> AddAsync(Faction faction);

    /// <summary>
    ///     Checks whether a faction with the specified name exists.
    /// </summary>
    /// <param name="name">The name of the faction to check.</param>
    /// <param name="factionId">The unique id of the faction to check.</param>
    /// <returns>
    ///     A task representing the asynchronous operation, containing <see langword="true" /> if a faction with the given name
    ///     exists; otherwise, <see langword="false" />.
    /// </returns>
    Task<bool> ExistsWithNameAsync(string name, Guid factionId);

    /// <summary>
    ///     Checks whether a faction with the specified code exists.
    /// </summary>
    /// <param name="code">The unique code of the faction to check.</param>
    /// <param name="factionId">The unique id of the faction to check.</param>
    /// <returns>
    ///     A task representing the asynchronous operation, containing <see langword="true" /> if a faction with the given code
    ///     exists; otherwise, <see langword="false" />.
    /// </returns>
    Task<bool> ExistsWithCodeAsync(string code, Guid factionId);
    
    /// <summary>
    /// Retrieves a list of factions with their owned resources, sorted by the specified parameters.
    /// </summary>
    /// <param name="sortBy">The field by which to sort (e.g., "name", "code").</param>
    /// <param name="sortOrder">The sort direction ("asc" or "desc").</param>
    /// <returns>
    /// A task representing the asynchronous operation, containing a sorted list of <see cref="Faction"/> entities.
    /// </returns>
    Task<List<Faction>> GetWithResourcesAsync(string sortBy, string sortOrder);

    /// <summary>
    ///     Retrieves a faction by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the faction.</param>
    /// <returns>
    ///     A task representing the asynchronous operation, containing the <see cref="Faction" /> if found; otherwise,
    ///     <see langword="null" />.
    /// </returns>
    Task<Faction?> GetByIdWithResourcesAsync(Guid id);

    /// <summary>
    ///     Updates an existing faction asynchronously.
    /// </summary>
    /// <param name="faction">The faction with updated data.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task UpdateAsync(Faction faction);
}