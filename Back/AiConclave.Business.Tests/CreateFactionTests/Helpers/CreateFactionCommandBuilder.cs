using AiConclave.Business.Application.Factions;
using AiConclave.Business.Application.Factions.DTOs;
using AiConclave.Business.Domain.Model;

namespace AiConclave.Business.Tests.CreateFactionTests.Helpers;

/// <summary>
///     Builder class for creating instances of <see cref="CreateFactionCommand" />.
///     Provides methods to customize command properties for testing.
/// </summary>
public class CreateFactionCommandBuilder
{
    private readonly CreateFactionCommand _command;

    /// <summary>
    ///     Initializes a new instance of the <see cref="CreateFactionCommandBuilder" /> class.
    /// </summary>
    /// <param name="presenter">The test presenter to be used in the command.</param>
    public CreateFactionCommandBuilder(TestPresenter<CreateFactionResponse> presenter)
    {
        _command = new CreateFactionCommand(
            "TST", // Default code
            "TestName", // Default name
            "TestDescription", // Default description
            Resource.All.Select(r => 
            {
                var amount = r.Code == Resource.Co2.Code ? 0 : 10;
                return new ResourceAmountDto(r.Code, amount);
            }).ToList(), // Default resources
            presenter
        );
    }

    /// <summary>
    ///     Sets the faction code to use in the command.
    /// </summary>
    /// <param name="code">The faction code.</param>
    /// <returns>The current <see cref="CreateFactionCommandBuilder" /> instance.</returns>
    public CreateFactionCommandBuilder WithCode(string code)
    {
        _command.Code = code;
        return this;
    }

    /// <summary>
    ///     Sets the faction name to use in the command.
    /// </summary>
    /// <param name="name">The faction name.</param>
    /// <returns>The current <see cref="CreateFactionCommandBuilder" /> instance.</returns>
    public CreateFactionCommandBuilder WithName(string name)
    {
        _command.Name = name;
        return this;
    }

    /// <summary>
    ///     Sets the faction description to use in the command.
    /// </summary>
    /// <param name="description">The faction description.</param>
    /// <returns>The current <see cref="CreateFactionCommandBuilder" /> instance.</returns>
    public CreateFactionCommandBuilder WithDescription(string description)
    {
        _command.Description = description;
        return this;
    }

    /// <summary>
    ///     Sets or updates the amount for a specific resource in the command.
    /// </summary>
    /// <param name="resource">The resource to set or update.</param>
    /// <param name="amount">The amount to assign to the resource.</param>
    /// <returns>The current <see cref="CreateFactionCommandBuilder" /> instance.</returns>
    public CreateFactionCommandBuilder WithResource(Resource resource, int amount)
    {
        var existing = _command.ResourceAmounts.FirstOrDefault(r => r.ResourceCode == resource.Code);
        if (existing != null)
            existing.Amount = amount;
        else
            _command.ResourceAmounts.Add(new ResourceAmountDto(resource.Code, amount));

        return this;
    }

    /// <summary>
    ///     Replaces the full list of resource amounts to be used in the command.
    /// </summary>
    /// <param name="amounts">A list of <see cref="ResourceAmountDto" /> representing the resource allocations.</param>
    /// <returns>The current <see cref="CreateFactionCommandBuilder" /> instance.</returns>
    public CreateFactionCommandBuilder WithResourceAmounts(List<ResourceAmountDto> amounts)
    {
        _command.ResourceAmounts = amounts;
        return this;
    }

    /// <summary>
    ///     Builds a <see cref="CreateFactionCommand" /> instance with the configured values.
    /// </summary>
    /// <returns>A new <see cref="CreateFactionCommand" />The constructed command instance.</returns>
    public CreateFactionCommand Build()
    {
        return _command;
    }
}