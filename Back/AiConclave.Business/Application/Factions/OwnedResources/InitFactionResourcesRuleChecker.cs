using AiConclave.Business.Domain.Entities;
using AiConclave.Business.Domain.RuleCheckers;

namespace AiConclave.Business.Application.Factions.OwnedResources;

/// <summary>
///     Rule checker for validating resource initialization rules of a <see cref="Faction" />.
/// </summary>
public class InitFactionResourcesRuleChecker : BaseRuleChecker<Faction>
{
    public const int ExpectedInitialTotal = 60;

    /// <summary>
    ///     Initializes a new instance of the <see cref="InitFactionResourcesRuleChecker" /> class.
    /// </summary>
    public InitFactionResourcesRuleChecker()
    {
        InitializeRules();
    }

    /// <summary>
    ///     Adds validation rules for setting initial faction resources.
    /// </summary>
    protected override void AddRules()
    {
        AddRule(new TotalResourceAmountEqualsSpec(ExpectedInitialTotal));
        AddRule(new InitialCo2MustBePositiveSpec());
    }
}