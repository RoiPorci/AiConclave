using AiConclave.Business.Application.Factions;

namespace AiConclave.Business.Tests.CreateFactionTests.Helpers;

/// <summary>
///     Builder class for creating instances of <see cref="CreateFactionCommand" />.
///     Provides methods to customize command properties for testing.
/// </summary>
public class CreateFactionCommandBuilder
{
    private readonly TestPresenter<CreateFactionResponse> _presenter;
    private string _code = "VNM";
    private string _description = "WE ARE VENOM!";
    private string _name = "Venom";

    /// <summary>
    ///     Initializes a new instance of the <see cref="CreateFactionCommandBuilder" /> class.
    /// </summary>
    /// <param name="presenter">The presenter responsible for handling the response.</param>
    public CreateFactionCommandBuilder(TestPresenter<CreateFactionResponse> presenter)
    {
        _presenter = presenter;
    }

    /// <summary>
    ///     Sets the faction code for the command.
    /// </summary>
    /// <param name="code">The faction code.</param>
    /// <returns>The updated <see cref="CreateFactionCommandBuilder" /> instance.</returns>
    public CreateFactionCommandBuilder WithCode(string code)
    {
        _code = code;
        return this;
    }

    /// <summary>
    ///     Sets the faction name for the command.
    /// </summary>
    /// <param name="name">The faction name.</param>
    /// <returns>The updated <see cref="CreateFactionCommandBuilder" /> instance.</returns>
    public CreateFactionCommandBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    /// <summary>
    ///     Sets the faction description for the command.
    /// </summary>
    /// <param name="description">The faction description.</param>
    /// <returns>The updated <see cref="CreateFactionCommandBuilder" /> instance.</returns>
    public CreateFactionCommandBuilder WithDescription(string description)
    {
        _description = description;
        return this;
    }

    /// <summary>
    ///     Builds and returns a <see cref="CreateFactionCommand" /> instance with the configured values.
    /// </summary>
    /// <returns>A new instance of <see cref="CreateFactionCommand" />.</returns>
    public CreateFactionCommand Build()
    {
        return new CreateFactionCommand(_code, _name, _description, _presenter);
    }
}