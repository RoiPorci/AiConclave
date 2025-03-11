using Business.Domain.Entities;
using Business.Domain.Specifications;

namespace Business.Domain.RuleCheckers;

/// <summary>
/// Rule checker for validating <see cref="Faction"/> entities.
/// </summary>
public class FactionRuleChecker : BaseRuleChecker<Faction>
{
    /// <summary>
    /// Adds validation rules specific to factions.
    /// </summary>
    protected override void AddRules()
    {
        AddRule(new FactionNameRequiredSpec());
    }
}