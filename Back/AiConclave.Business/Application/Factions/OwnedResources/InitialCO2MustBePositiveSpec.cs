using System.Threading.Tasks;
using AiConclave.Business.Domain.Entities;
using AiConclave.Business.Domain.Model;
using AiConclave.Business.Domain.Specifications;

namespace AiConclave.Business.Application.Factions.OwnedResources;

/// <summary>
/// Specification that ensures the CO2 resource of a <see cref="Faction"/> is present and non-negative at initialization.
/// </summary>
public class InitialCO2MustBePositiveSpec : ISpecification<Faction>
{
    /// <summary>
    /// Validates that the CO2 resource exists and its amount is not negative.
    /// </summary>
    /// <param name="faction">The faction to validate.</param>
    /// <returns>
    /// A <see cref="ValidationResult"/> containing an error if the CO2 amount is negative
    /// or if the CO2 resource is missing.
    /// </returns>
    public Task<ValidationResult> ValidateAsync(Faction faction)
    {
        var result = new ValidationResult();

        if (faction.OwnedResources.TryGetValue(Resource.CO2.Code, out var co2Resource))
        {
            if (co2Resource.Amount < 0)
            {
                result.Errors.Add("CO2 cannot be negative at initialization.");
            }
        }
        else
        {
            result.Errors.Add("CO2 resource is missing from faction.");
        }

        return Task.FromResult(result);
    }
}