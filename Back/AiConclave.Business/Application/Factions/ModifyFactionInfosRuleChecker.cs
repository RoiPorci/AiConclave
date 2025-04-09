using AiConclave.Business.Application.Factions.Specs;
using AiConclave.Business.Domain.Entities;
using AiConclave.Business.Domain.Repositories;
using AiConclave.Business.Domain.RuleCheckers;

namespace AiConclave.Business.Application.Factions;

/// <summary>
/// Rule checker for validating modifications to a <see cref="Faction"/>'s basic information (e.g. name, code).
/// </summary>
public class ModifyFactionInfosRuleChecker : BaseRuleChecker<Faction>
{
    private readonly IFactionRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="ModifyFactionInfosRuleChecker"/> class.
    /// </summary>
    /// <param name="repository">The repository used to validate uniqueness constraints.</param>
    public ModifyFactionInfosRuleChecker(IFactionRepository repository)
    {
        _repository = repository;
        InitializeRules();
    }

    /// <summary>
    /// Adds validation rules for modifying faction information.
    /// </summary>
    protected override void AddRules()
    {
        AddRule(new UniqueFactionSpec(_repository));
    }
}