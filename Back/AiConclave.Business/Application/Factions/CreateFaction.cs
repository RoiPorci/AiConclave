using System.Threading.Tasks;
using AiConclave.Business.Domain.Entities;
using AiConclave.Business.Domain.Repositories;
using AiConclave.Business.Domain.RuleCheckers;
using AiConclave.Business.Domain.Specifications;

namespace AiConclave.Business.Application.Factions;

/// <summary>
/// Use case for creating a new <see cref="Faction"/>.
/// </summary>
public class CreateFaction : IUseCase<CreateFactionRequest, CreateFactionResponse>
{
    private readonly IFactionRepository _repository;
    private readonly FactionRuleChecker _factionRuleChecker;
    private readonly CreateFactionRuleChecker _createFactionRuleChecker;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateFaction"/> class.
    /// </summary>
    /// <param name="repository">The repository used to manage factions.</param>
    public CreateFaction(
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
    /// Executes the use case to create a faction.
    /// </summary>
    /// <param name="request">The request containing faction details.</param>
    /// <param name="presenter">The presenter used to return the response.</param>
    public async Task ExecuteAsync(CreateFactionRequest request, IUseCasePresenter<CreateFactionResponse> presenter)
    {
        var response = new CreateFactionResponse();

        // 1. Build the faction
        var faction = Faction.Create(request.Code, request.Name, request.Description);

        // 2. Validate the faction
        var validationResult = await ValidateAsync(faction);

        if (!validationResult.IsValid)
        {
            response.Errors = validationResult.Errors;
            presenter.Present(response);
            return;
        }

        // 3. Save the faction
        await _repository.AddAsync(faction);

        // 4. Present the response
        BuildResponse(response, faction);
        presenter.Present(response);
    }

    /// <summary>
    /// Validates the faction using rule checkers.
    /// </summary>
    /// <param name="faction">The faction to validate.</param>
    /// <returns>
    /// A <see cref="ValidationResult"/> indicating whether the faction is valid.
    /// </returns>
    private async Task<ValidationResult> ValidateAsync(Faction faction)
    {
        var validationResult = await _factionRuleChecker.ValidateAsync(faction);

        if (!validationResult.IsValid)
            return validationResult;
        
        validationResult = await _createFactionRuleChecker.ValidateAsync(faction);
        
        return validationResult;
    }

    /// <summary>
    /// Builds the response object based on the created faction.
    /// </summary>
    /// <param name="response">The response to populate.</param>
    /// <param name="faction">The created faction.</param>
    private void BuildResponse(CreateFactionResponse response, Faction faction)
    {
        response.FactionId = faction.Id;
        response.Code = faction.Code;
        response.Name = faction.Name;
        response.Description = faction.Description;
    }
}
