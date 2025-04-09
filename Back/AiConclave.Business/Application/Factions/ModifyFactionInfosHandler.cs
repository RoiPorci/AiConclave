using System.Threading;
using System.Threading.Tasks;
using AiConclave.Business.Domain.Entities;
using AiConclave.Business.Domain.Repositories;
using AiConclave.Business.Domain.RuleCheckers;
using AiConclave.Business.Domain.Specifications;

namespace AiConclave.Business.Application.Factions;

/// <summary>
/// Handler for modifying the basic information of a <see cref="Faction"/>.
/// </summary>
public class ModifyFactionInfosHandler : BaseHandler<ModifyFactionInfosCommand, ModifyFactionInfosResponse>
{
    private readonly IFactionRepository _repository;
    private readonly ModifyFactionInfosRuleChecker _modifyFactionInfosRuleChecker;
    private readonly FactionRuleChecker _factionRuleChecker;

    /// <summary>
    /// Initializes a new instance of the <see cref="ModifyFactionInfosHandler"/> class.
    /// </summary>
    /// <param name="ruleChecker">The rule checker for faction uniqueness validation.</param>
    /// <param name="repository">The repository for accessing and updating factions.</param>
    /// <param name="factionRuleChecker">The domain-level faction rule checker.</param>
    public ModifyFactionInfosHandler(
        ModifyFactionInfosRuleChecker ruleChecker,
        IFactionRepository repository,
        FactionRuleChecker factionRuleChecker)
    {
        _modifyFactionInfosRuleChecker = ruleChecker;
        _repository = repository;
        _factionRuleChecker = factionRuleChecker;
    }

    /// <summary>
    /// Handles the request to modify a faction's name, code, or description.
    /// </summary>
    /// <param name="request">The command containing updated faction information.</param>
    /// <param name="cancellationToken">Token for cancelling the operation.</param>
    /// <returns>A <see cref="ModifyFactionInfosResponse"/> with updated data or validation errors.</returns>
    protected override async Task<ModifyFactionInfosResponse> HandleRequest(ModifyFactionInfosCommand request, CancellationToken cancellationToken)
    {
        var response = new ModifyFactionInfosResponse();

        // 1. Retrieve the faction
        var factionToUpdate = await _repository.GetByIdWithResourcesAsync(request.FactionId);
        if (factionToUpdate == null)
        {
            response.Errors.Add($"Faction with ID {request.FactionId} not found.");
            return response;
        }

        // 2. Modify the faction infos
        if (request.Name != null) factionToUpdate.Name = request.Name;
        if (request.Code != null) factionToUpdate.Code = request.Code;
        if (request.Description != null) factionToUpdate.Description = request.Description;

        // 3. Validate the modified faction
        var factionValidation = await ValidateFactionAsync(factionToUpdate);
        if (!factionValidation.IsValid)
        {
            response.Errors.AddRange(factionValidation.Errors);
            return response;
        }

        // 4. Update the faction
        await _repository.UpdateAsync(factionToUpdate);

        // 5. Build and return the response
        BuildResponse(response, factionToUpdate);
        return response;
    }

    /// <summary>
    /// Validates the faction using both general and update-specific rule checkers.
    /// </summary>
    /// <param name="faction">The faction to validate.</param>
    /// <returns>
    /// A <see cref="ValidationResult"/> indicating whether the faction is valid.
    /// </returns>
    private async Task<ValidationResult> ValidateFactionAsync(Faction faction)
    {
        var validationResult = await _factionRuleChecker.ValidateAsync(faction);
        if (!validationResult.IsValid)
            return validationResult;

        return await _modifyFactionInfosRuleChecker.ValidateAsync(faction);
    }

    /// <summary>
    /// Builds the response object based on the modified faction.
    /// </summary>
    /// <param name="response">The response to populate.</param>
    /// <param name="faction">The modified faction.</param>
    private static void BuildResponse(ModifyFactionInfosResponse response, Faction faction)
    {
        response.FactionId = faction.Id;
        response.Code = faction.Code;
        response.Name = faction.Name;
        response.Description = faction.Description;
    }
}
