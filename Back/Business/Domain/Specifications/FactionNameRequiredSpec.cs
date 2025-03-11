using System.Threading.Tasks;
using Business.Domain.Entities;

namespace Business.Domain.Specifications;

/// <summary>
/// Specification that ensures a <see cref="Faction"/> has a non-empty name.
/// </summary>
public class FactionNameRequiredSpec : ISpecification<Faction>
{
    /// <summary>
    /// Validates that the <see cref="Faction"/> has a name.
    /// </summary>
    /// <param name="faction">The faction to validate.</param>
    /// <returns>
    /// A <see cref="ValidationResult"/> containing an error if the name is missing or whitespace.
    /// </returns>
    public Task<ValidationResult> ValidateAsync(Faction faction)
    {
        var result = new ValidationResult();

        if (string.IsNullOrWhiteSpace(faction.Name))
            result.AddError("Faction name is required.");

        return Task.FromResult(result);
    }
}