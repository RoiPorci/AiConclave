using System.Threading.Tasks;
using AiConclave.Business.Domain.Entities;

namespace AiConclave.Business.Domain.Specifications;

/// <summary>
///     Specification that validates resource amounts in a <see cref="Faction" />.
///     Ensures that resources which do not allow negative values are non-negative.
/// </summary>
public class ResourceAmountsSignSpec : ISpecification<Faction>
{
    /// <summary>
    ///     Validates that no resource in the faction has a negative amount if it is not allowed.
    /// </summary>
    /// <param name="faction">The faction to validate.</param>
    /// <returns>
    ///     A <see cref="ValidationResult" /> containing validation errors if any resource has an invalid amount.
    /// </returns>
    public Task<ValidationResult> ValidateAsync(Faction faction)
    {
        var result = new ValidationResult();

        foreach (var owned in faction.OwnedResources.Values)
        {
            var resource = owned.Resource;

            if (!resource.AllowsNegativeValues && owned.Amount < 0)
                result.Errors.Add($"Resource '{resource.Name}' cannot have a negative amount.");
        }

        return Task.FromResult(result);
    }
}