using System.Threading.Tasks;
using Business.Domain.Entities;

namespace Business.Domain.Specifications;

public class FactionNameRequiredSpec : ISpecification<Faction>
{
    public Task<ValidationResult> ValidateAsync(Faction faction)
    {
        var result = new ValidationResult();

        if (string.IsNullOrWhiteSpace(faction.Name))
            result.AddError("Faction name is required.");

        return Task.FromResult(result);
    }
}