using System.Threading.Tasks;
using Business.Domain.Entities;
using Business.Domain.Repositories;

namespace Business.Application.Factions;

public class CreateFaction : IUseCase<CreateFactionRequest, CreateFactionResponse>
{
    private readonly IFactionRepository _repository;

    public CreateFaction(IFactionRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(CreateFactionRequest request, IUseCasePresenter<CreateFactionResponse> presenter)
    {
        var response = new CreateFactionResponse();

        // 1. Build the faction
        var faction = Faction.Create(request.Name, request.Description);

        // 2. Validate the faction

        // 3. Save the faction
        await _repository.AddAsync(faction);

        // 4. Present the response
        response.FactionId = faction.Id;
        response.Name = faction.Name;
        response.Description = faction.Description;

        presenter.Present(response);
    }
}