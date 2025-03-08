namespace Business.Application.Factions;

public class CreateFactionRequest : IUseCaseRequest
{
    public string Name { get; set; }
    public string Description { get; set; } 
}