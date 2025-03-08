using System;

namespace Business.Application.Factions;

public class CreateFactionResponse : UseCaseResponse
{
    public Guid FactionId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}