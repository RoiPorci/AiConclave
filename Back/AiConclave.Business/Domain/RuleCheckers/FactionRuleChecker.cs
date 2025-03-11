using AiConclave.Business.Domain.Entities;
using AiConclave.Business.Domain.Specifications;

namespace AiConclave.Business.Domain.RuleCheckers;

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
        AddRule(new FactionFieldRequiredSpec());
    }
}