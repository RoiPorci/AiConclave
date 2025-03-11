using System.Threading.Tasks;
using AiConclave.Business.Domain.Entities;

namespace AiConclave.Business.Domain.Repositories;

/// <summary>
/// Repository interface for managing <see cref="Faction"/> entities.
/// </summary>
public interface IFactionRepository
{
    /// <summary>
    /// Retrieves a faction by its name asynchronously.
    /// </summary>
    /// <param name="name">The name of the faction.</param>
    /// <returns>
    /// A task representing the asynchronous operation, containing the <see cref="Faction"/> if found; otherwise, <see langword="null"/>.
    /// </returns>
    Task<Faction?> GetByNameAsync(string name);

    /// <summary>
    /// Adds a new faction asynchronously.
    /// </summary>
    /// <param name="faction">The faction to add.</param>
    /// <returns>
    /// A task representing the asynchronous operation, containing the added <see cref="Faction"/>.
    /// </returns>
    Task<Faction> AddAsync(Faction faction);
    
    Task<bool> ExistsWithNameAsync(string name);
    
    Task<bool> ExistsWithCodeAsync(string code);
}