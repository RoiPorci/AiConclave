using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Domain.Specifications;

namespace Business.Domain.RuleCheckers;

public abstract class BaseRuleChecker<T>
{
    private readonly List<ISpecification<T>> _rules = new();

    protected BaseRuleChecker()
    {
        AddRules();
    }
    
    protected abstract void AddRules();
    
    protected void AddRule(ISpecification<T> rule)
    {
        _rules.Add(rule);
    }

    public async Task<ValidationResult> ValidateAsync(T entity)
    {
        var validationResult = new ValidationResult();

        foreach (var rule in _rules)
        {
            var result = await rule.ValidateAsync(entity);
            validationResult.Errors.AddRange(result.Errors);
        }

        return validationResult;
    }
}



