using System;
using System.Threading.Tasks;
using AiConclave.Business.Domain.Entities;

namespace AiConclave.Business.Domain.Repositories;

/// <summary>
/// Repository interface for managing <see cref="Faction"/> entities.
/// </summary>
public interface IFactionRepository
{
    /// <summary>
    /// Adds a new faction asynchronously.
    /// </summary>
    /// <param name="faction">The faction to add.</param>
    /// <returns>
    /// A task representing the asynchronous operation, containing the added <see cref="Faction"/>.
    /// </returns>
    Task<Faction> AddAsync(Faction faction);
    
    /// <summary>
    /// Checks whether a faction with the specified name exists.
    /// </summary>
    /// <param name="name">The name of the faction to check.</param>
    /// <returns>
    /// A task representing the asynchronous operation, containing <see langword="true"/> if a faction with the given name exists; otherwise, <see langword="false"/>.
    /// </returns>
    Task<bool> ExistsWithNameAsync(string name);
    
    /// <summary>
    /// Checks whether a faction with the specified code exists.
    /// </summary>
    /// <param name="code">The unique code of the faction to check.</param>
    /// <returns>
    /// A task representing the asynchronous operation, containing <see langword="true"/> if a faction with the given code exists; otherwise, <see langword="false"/>.
    /// </returns>
    Task<bool> ExistsWithCodeAsync(string code);

    /// <summary>
    /// Retrieves a faction by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the faction.</param>
    /// <returns>
    /// A task representing the asynchronous operation, containing the <see cref="Faction"/> if found; otherwise, <see langword="null"/>.
    /// </returns>
    Task<Faction?> GetByIdAsync(Guid id);

    /// <summary>
    /// Updates an existing faction asynchronously.
    /// </summary>
    /// <param name="faction">The faction with updated data.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task UpdateAsync(Faction faction);
}
