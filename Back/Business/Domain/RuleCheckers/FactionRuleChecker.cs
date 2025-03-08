using Business.Domain.Entities;
using Business.Domain.Specifications;

namespace Business.Domain.RuleCheckers;

public class FactionRuleChecker : BaseRuleChecker<Faction>
{
    protected override void AddRules()
    {
        AddRule(new FactionNameRequiredSpec());
    }
}