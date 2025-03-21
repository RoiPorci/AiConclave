using System.Threading;
using System.Threading.Tasks;
using AiConclave.Business.Domain.Entities;
using AiConclave.Business.Domain.Repositories;
using AiConclave.Business.Domain.RuleCheckers;
using AiConclave.Business.Domain.Specifications;

namespace AiConclave.Business.Application.Factions;

/// <summary>
///     Use case for creating a new <see cref="Faction" />.
/// </summary>
public class CreateFactionHandler : BaseHandler<CreateFactionCommand, CreateFactionResponse>
{
    private readonly CreateFactionRuleChecker _createFactionRuleChecker;
    private readonly FactionRuleChecker _factionRuleChecker;
    private readonly IFactionRepository _repository;

    /// <summary>
    ///     Initializes a new instance of the <see cref="CreateFactionHandler" /> class.
    /// </summary>
    /// <param name="repository">The repository used to manage factions.</param>
    /// <param name="factionRuleChecker">The general rule checker for faction validation.</param>
    /// <param name="createFactionRuleChecker">The rule checker for creation-specific validation.</param>
    public CreateFactionHandler(
        IFactionRepository repository,
        FactionRuleChecker factionRuleChecker,
        CreateFactionRuleChecker createFactionRuleChecker
    )
    {
        _repository = repository;
        _factionRuleChecker = factionRuleChecker;
        _createFactionRuleChecker = createFactionRuleChecker;
    }

    /// <summary>
    ///     Handles the creation of a new faction.
    /// </summary>
    /// <param name="command">The command containing the faction details.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     A task representing the asynchronous operation, returning a <see cref="CreateFactionResponse" /> instance.
    /// </returns>
    protected override async Task<CreateFactionResponse> HandleRequest(CreateFactionCommand command,
        CancellationToken cancellationToken)
    {
        var response = new CreateFactionResponse();

        // 1. Build the faction
        var faction = Faction.Create(command.Code, command.Name, command.Description);

        // 2. Validate the faction
        var validationResult = await ValidateAsync(faction);

        if (!validationResult.IsValid)
        {
            response.Errors = validationResult.Errors;
            return response;
        }

        // 3. Save the faction
        await _repository.AddAsync(faction);

        // 4. Build and return the response
        BuildResponse(response, faction);
        return response;
    }

    /// <summary>
    ///     Validates the faction using rule checkers.
    /// </summary>
    /// <param name="faction">The faction to validate.</param>
    /// <returns>
    ///     A <see cref="ValidationResult" /> indicating whether the faction is valid.
    /// </returns>
    private async Task<ValidationResult> ValidateAsync(Faction faction)
    {
        var validationResult = await _factionRuleChecker.ValidateAsync(faction);

        if (!validationResult.IsValid)
            return validationResult;

        return await _createFactionRuleChecker.ValidateAsync(faction);
    }

    /// <summary>
    ///     Builds the response object based on the created faction.
    /// </summary>
    /// <param name="response">The response to populate.</param>
    /// <param name="faction">The created faction.</param>
    private static void BuildResponse(CreateFactionResponse response, Faction faction)
    {
        response.FactionId = faction.Id;
        response.Code = faction.Code;
        response.Name = faction.Name;
        response.Description = faction.Description;
    }
}