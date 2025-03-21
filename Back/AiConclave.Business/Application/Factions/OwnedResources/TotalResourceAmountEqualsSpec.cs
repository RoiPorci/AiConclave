using System.Linq;
using System.Threading.Tasks;
using AiConclave.Business.Domain.Entities;
using AiConclave.Business.Domain.Specifications;

namespace AiConclave.Business.Application.Factions.OwnedResources;

/// <summary>
/// Specification that checks if the total amount of resources owned by a <see cref="Faction"/> equals a given value.
/// </summary>
public class TotalResourceAmountEqualsSpec : ISpecification<Faction>
{
    private readonly int _expectedTotal;

    /// <summary>
    /// Initializes a new instance of the <see cref="TotalResourceAmountEqualsSpec"/> class.
    /// </summary>
    /// <param name="expectedTotal">The expected total sum of all resource amounts.</param>
    public TotalResourceAmountEqualsSpec(int expectedTotal)
    {
        _expectedTotal = expectedTotal;
    }

    /// <summary>
    /// Validates that the total amount of resources in the faction equals the expected value.
    /// </summary>
    /// <param name="faction">The faction to validate.</param>
    /// <returns>
    /// A <see cref="ValidationResult"/> indicating whether the total amount is correct.
    /// </returns>
    public Task<ValidationResult> ValidateAsync(Faction faction)
    {
        var result = new ValidationResult();

        var total = faction.OwnedResources.Values.Sum(or => or.Amount);
        if (total != _expectedTotal)
        {
            result.Errors.Add($"The total amount of resources must equal {_expectedTotal}, but is {total}.");
        }

        return Task.FromResult(result);
    }
}