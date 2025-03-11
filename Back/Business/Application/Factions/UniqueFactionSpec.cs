using System.Threading.Tasks;
using AiConclave.Business.Domain.Entities;
using AiConclave.Business.Domain.Repositories;
using AiConclave.Business.Domain.Specifications;

namespace AiConclave.Business.Application.Factions;

/// <summary>
/// Specification to ensure that a <see cref="Faction"/> has a unique code and name.
/// </summary>
public class UniqueFactionSpec : ISpecification<Faction>
{
    private readonly IFactionRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="UniqueFactionSpec"/> class.
    /// </summary>
    /// <param name="repository">The repository used to check for existing factions.</param>
    public UniqueFactionSpec(IFactionRepository repository)
    {
        _repository = repository;
    }
    
    /// <summary>
    /// Validates whether the given <see cref="Faction"/> has a unique code and name.
    /// </summary>
    /// <param name="faction">The faction to validate.</param>
    /// <returns>
    /// A <see cref="ValidationResult"/> indicating whether the faction is valid.
    /// </returns>
    public async Task<ValidationResult> ValidateAsync(Faction faction)
    {
        var result = new ValidationResult();
        
        if (await _repository.ExistsWithCodeAsync(faction.Code))
            result.AddError("Code already exists");
        
        if (await _repository.ExistsWithNameAsync(faction.Name))
            result.AddError("Name already exists");

        return result;
    }
}