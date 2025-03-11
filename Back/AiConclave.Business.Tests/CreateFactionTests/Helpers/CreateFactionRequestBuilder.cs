using AiConclave.Business.Application.Factions;

namespace AiConclave.Business.Tests.CreateFactionTests.Helpers;

public class CreateFactionRequestBuilder
{
    private string _code = "NANO";
    private string _name = "Mechabellum";
    private string _description  = "Beep beep boop... NANO EXPLOSION !! FIRE MECHA-LASER : PEEEEeeeeeeEEEEEEEWWWW !";

    public CreateFactionRequestBuilder WithCode(string code)
    {
        _code = code;
        return this;
    }

    public CreateFactionRequestBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public CreateFactionRequestBuilder WithDescription(string description)
    {
        _description = description;
        return this;
    }

    public CreateFactionRequest Build()
    {
        return new CreateFactionRequest
        {
            Code = _code,
            Name = _name,
            Description = _description
        };
    }
}