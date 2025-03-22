using AiConclave.Business.Domain.Entities;
using AiConclave.Business.Domain.Repositories;
using AiConclave.Business.Domain.RuleCheckers;

namespace AiConclave.Business.Application.Factions;

/// <summary>
///     Rule checker for validating the creation of a <see cref="Faction" />.
///     Ensures that the faction meets the necessary validation rules before being created.
/// </summary>
public class CreateFactionRuleChecker : BaseRuleChecker<Faction>
{
    private readonly IFactionRepository _repository;

    /// <summary>
    ///     Initializes a new instance of the <see cref="CreateFactionRuleChecker" /> class.
    /// </summary>
    /// <param name="repository">The repository used to check for existing factions.</param>
    public CreateFactionRuleChecker(IFactionRepository repository)
    {
        _repository = repository;
        InitializeRules();
    }

    /// <summary>
    ///     Adds validation rules to ensure the faction is valid for creation.
    /// </summary>
    protected override void AddRules()
    {
        AddRule(new UniqueFactionSpec(_repository));
    }
}