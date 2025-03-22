using System.Collections.Generic;
using System.Threading.Tasks;
using AiConclave.Business.Domain.Specifications;

namespace AiConclave.Business.Domain.RuleCheckers;

/// <summary>
///     Base class for rule checkers that validate entities against a set of specifications.
/// </summary>
/// <typeparam name="T">The type of entity being validated.</typeparam>
public abstract class BaseRuleChecker<T>
{
    private readonly List<ISpecification<T>> _rules = [];

    /// <summary>
    ///     Defines the set of rules that should be applied for validation.
    ///     Implementations should call <see cref="AddRule" /> to add rules.
    /// </summary>
    protected abstract void AddRules();

    /// <summary>
    ///     Adds a validation rule to the rule checker.
    /// </summary>
    /// <param name="rule">The validation rule to add.</param>
    protected void AddRule(ISpecification<T> rule)
    {
        _rules.Add(rule);
    }

    protected void InitializeRules()
    {
        AddRules();
    }

    /// <summary>
    ///     Validates the given entity against all defined rules asynchronously.
    /// </summary>
    /// <param name="entity">The entity to validate.</param>
    /// <returns>
    ///     A <see cref="ValidationResult" /> containing any validation errors.
    /// </returns>
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