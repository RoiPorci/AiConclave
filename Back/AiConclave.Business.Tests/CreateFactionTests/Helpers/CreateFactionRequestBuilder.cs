using AiConclave.Business.Application.Factions;

namespace AiConclave.Business.Tests.CreateFactionTests.Helpers;

/// <summary>
/// Builder class for creating instances of <see cref="CreateFactionRequest"/>.
/// Provides methods to customize request properties for testing.
/// </summary>
public class CreateFactionRequestBuilder
{
    private string _code = "VNM";
    private string _name = "Venom";
    private string _description = "WE ARE VENOM!";

    /// <summary>
    /// Sets the faction code for the request.
    /// </summary>
    /// <param name="code">The faction code.</param>
    /// <returns>The updated <see cref="CreateFactionRequestBuilder"/> instance.</returns>
    public CreateFactionRequestBuilder WithCode(string code)
    {
        _code = code;
        return this;
    }

    /// <summary>
    /// Sets the faction name for the request.
    /// </summary>
    /// <param name="name">The faction name.</param>
    /// <returns>The updated <see cref="CreateFactionRequestBuilder"/> instance.</returns>
    public CreateFactionRequestBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    /// <summary>
    /// Sets the faction description for the request.
    /// </summary>
    /// <param name="description">The faction description.</param>
    /// <returns>The updated <see cref="CreateFactionRequestBuilder"/> instance.</returns>
    public CreateFactionRequestBuilder WithDescription(string description)
    {
        _description = description;
        return this;
    }

    /// <summary>
    /// Builds and returns a <see cref="CreateFactionRequest"/> instance with the configured values.
    /// </summary>
    /// <returns>A new instance of <see cref="CreateFactionRequest"/>.</returns>
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