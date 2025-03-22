using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AiConclave.Business.Domain.Entities;
using AiConclave.Business.Domain.Repositories;
using AiConclave.Business.Domain.RuleCheckers;
using AiConclave.Business.Domain.Specifications;

namespace AiConclave.Business.Application.Factions.OwnedResources;

/// <summary>
/// Handler for setting the initial resources of a <see cref="Faction"/>.
/// </summary>
public class InitFactionResourcesHandler : BaseHandler<InitFactionResourcesCommand, InitFactionResourcesResponse>
{
    private readonly IFactionRepository _repository;
    private readonly InitFactionResourcesRuleChecker _ruleChecker;
    private readonly FactionResourcesRuleChecker _factionResourcesRuleChecker;

    /// <summary>
    /// Initializes a new instance of the <see cref="InitFactionResourcesHandler"/> class.
    /// </summary>
    /// <param name="repository">The faction repository.</param>
    /// <param name="ruleChecker">Checker for initial resource allocation rules.</param>
    /// <param name="factionResourcesRuleChecker">Checker for general faction resource rules.</param>
    public InitFactionResourcesHandler(
        IFactionRepository repository,
        InitFactionResourcesRuleChecker ruleChecker, 
        FactionResourcesRuleChecker factionResourcesRuleChecker)
    {
        _repository = repository;
        _ruleChecker = ruleChecker;
        _factionResourcesRuleChecker = factionResourcesRuleChecker;
    }

    /// <summary>
    /// Handles the resource initialization command for a faction.
    /// </summary>
    /// <param name="command">The command containing resource allocations.</param>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="InitFactionResourcesResponse"/> containing the result of the operation.</returns>
    protected override async Task<InitFactionResourcesResponse> HandleRequest(
        InitFactionResourcesCommand command, 
        CancellationToken cancellationToken
    )
    {
        var response = new InitFactionResourcesResponse();

        // 1. Retrieve the faction
        var faction = await RetrieveFaction(command.FactionId, response);
        if (faction == null)
            return response;

        // 2. Apply the amounts
        ApplyAmounts(command, faction, response);
        if (!response.IsSuccess)
            return response;

        // 3. Validate the faction initial owned resources 
        var validationResult = await ValidateAsync(faction);
        if (!validationResult.IsValid)
        {
            response.Errors.AddRange(validationResult.Errors);
            return response;
        }

        // 4. Save changes
        await _repository.UpdateAsync(faction);

        // 5. Build and return the response
        BuildResponse(response, faction);
        return response;
    }

    /// <summary>
    /// Retrieves the faction by its identifier and adds an error to the response if not found.
    /// </summary>
    /// <param name="factionId">The ID of the faction to retrieve.</param>
    /// <param name="response">The response to populate in case of error.</param>
    /// <returns>The <see cref="Faction"/> if found; otherwise, <see langword="null"/>.</returns>
    private async Task<Faction?> RetrieveFaction(Guid factionId, InitFactionResourcesResponse response)
    {
        var faction = await _repository.GetByIdAsync(factionId);
        if (faction == null)
            response.Errors.Add($"Faction with ID {factionId} not found.");
        
        return faction;
    }

    /// <summary>
    /// Applies the provided resource amounts to the faction's owned resources.
    /// Adds errors to the response if any resource code is invalid.
    /// </summary>
    /// <param name="command">The command containing the resource amounts.</param>
    /// <param name="faction">The faction to update.</param>
    /// <param name="response">The response to populate in case of error.</param>
    private static void ApplyAmounts(InitFactionResourcesCommand command, Faction faction,
        InitFactionResourcesResponse response)
    {
        foreach (var resourceAmount in command.ResourceAmounts)
        {
            if (faction.OwnedResources.TryGetValue(resourceAmount.ResourceCode, out var ownedResource))
            {
                ownedResource.Amount = resourceAmount.Amount;
            }
            else
            {
                response.Errors.Add($"Invalid resource code: {resourceAmount.ResourceCode}");
            }
        }
    }

    /// <summary>
    /// Validates the faction using both general and context-specific resource rule checkers.
    /// </summary>
    /// <param name="faction">The faction to validate.</param>
    /// <returns>A <see cref="ValidationResult"/> containing validation errors, if any.</returns>
    private async Task<ValidationResult> ValidateAsync(Faction faction)
    {
        var validationResult = await _factionResourcesRuleChecker.ValidateAsync(faction);
        
        if (!validationResult.IsValid)
            return validationResult;
        
        return await _ruleChecker.ValidateAsync(faction);
    }

    /// <summary>
    /// Builds the response DTO based on the updated faction.
    /// </summary>
    /// <param name="response">The response object to populate.</param>
    /// <param name="faction">The updated faction entity.</param>
    private static void BuildResponse(InitFactionResourcesResponse response, Faction faction)
    {
        response.FactionId = faction.Id;
        response.UpdatedResources = faction.OwnedResources
            .Select(kvp => new ResourceAmountDto(kvp.Key, kvp.Value.Amount))
            .ToList();
    }
}
