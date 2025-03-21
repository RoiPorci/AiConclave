using AiConclave.Business.Domain.Entities;
using AiConclave.Business.Domain.Specifications;

namespace AiConclave.Business.Domain.RuleCheckers;

/// <summary>
/// Rule checker for validating resource-related rules in a <see cref="Faction"/>.
/// </summary>
public class FactionResourcesRuleChecker : BaseRuleChecker<Faction>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FactionResourcesRuleChecker"/> class.
    /// Sets up the resource validation rules.
    /// </summary>
    public FactionResourcesRuleChecker()
    {
        InitializeRules();
    }

    /// <summary>
    /// Adds resource-related validation rules for a <see cref="Faction"/>.
    /// </summary>
    protected override void AddRules()
    {
        AddRule(new ResourceAmountsSignSpec());
    }
}