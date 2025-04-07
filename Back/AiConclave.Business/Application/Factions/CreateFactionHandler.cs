using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AiConclave.Business.Domain.Entities;
using AiConclave.Business.Domain.Model;
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
    protected override async Task<CreateFactionResponse> HandleRequest(
        CreateFactionCommand command,
        CancellationToken cancellationToken)
    {
        var response = new CreateFactionResponse();

        // 1. Build the faction
        var faction = Faction.Create(command.Code, command.Name, command.Description);
        
        // 2. Apply custom resource amounts
        ApplyResourceAmounts(command.ResourceAmounts, faction, response);
        if (!response.IsSuccess)
            return response;

        // 3. Validate the faction
        var factionValidation = await ValidateFactionAsync(faction);
        if (!factionValidation.IsValid)
        {
            response.Errors.AddRange(factionValidation.Errors);
            return response;
        }
        
        // 4. Save the faction with its resources
        await _repository.AddAsync(faction);

        // 5. Build and return the response
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
    private async Task<ValidationResult> ValidateFactionAsync(Faction faction)
    {
        var validationResult = await _factionRuleChecker.ValidateAsync(faction);
        if (!validationResult.IsValid)
            return validationResult;

        return await _createFactionRuleChecker.ValidateAsync(faction);
    }

    /// <summary>
    ///     Applies the provided resource amounts to the faction's owned resources.
    ///     Adds errors to the response if any resource code is invalid.
    /// </summary>
    /// <param name="resourceAmounts">The resource amounts to apply.</param>
    /// <param name="faction">The faction to update.</param>
    /// <param name="response">The response to populate in case of error.</param>
    private static void ApplyResourceAmounts(List<ResourceAmountDto> resourceAmounts, Faction faction, CreateFactionResponse response)
    {
        foreach (var resourceAmount in resourceAmounts)
        {
            if (Resource.IsValidCode(resourceAmount.ResourceCode))
                faction.UpdateResourceAmount(resourceAmount.ResourceCode, resourceAmount.Amount);
            else
                response.Errors.Add($"Invalid resource code: {resourceAmount.ResourceCode}");
        }
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
        response.InitialResources = faction.OwnedResources
            .Select(kvp => new ResourceAmountDto(kvp.Key, kvp.Value.Amount))
            .ToList();
    }
}