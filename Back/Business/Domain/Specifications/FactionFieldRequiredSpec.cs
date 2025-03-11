using System.Threading.Tasks;
using Business.Domain.Entities;

namespace Business.Domain.Specifications;

/// <summary>
/// Specification that ensures a <see cref="Faction"/> has a non-empty name.
/// </summary>
public class FactionFieldRequiredSpec : ISpecification<Faction>
{
    /// <summary>
    /// Validates that the <see cref="Faction"/> has a code, a name and a description.
    /// </summary>
    /// <param name="faction">The faction to validate.</param>
    /// <returns>
    /// A <see cref="ValidationResult"/> containing an error if the name is missing or whitespace.
    /// </returns>
    public Task<ValidationResult> ValidateAsync(Faction faction)
    {
        var result = new ValidationResult();

        if (string.IsNullOrWhiteSpace(faction.Code))
            result.AddError("Faction code is required.");
        
        if (string.IsNullOrWhiteSpace(faction.Name))
            result.AddError("Faction name is required.");
        
        if (string.IsNullOrWhiteSpace(faction.Description))
            result.AddError("Faction description is required.");

        return Task.FromResult(result);
    }
}