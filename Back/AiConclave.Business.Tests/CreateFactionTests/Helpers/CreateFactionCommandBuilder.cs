using AiConclave.Business.Application.Factions;
using AiConclave.Business.Domain.Model;

namespace AiConclave.Business.Tests.CreateFactionTests.Helpers;

/// <summary>
///     Builder class for creating instances of <see cref="CreateFactionCommand" />.
///     Provides methods to customize command properties for testing.
/// </summary>
public class CreateFactionCommandBuilder
{
    private readonly TestPresenter<CreateFactionResponse> _presenter;
    private string _code = "TST";
    private string _name = "TestName";
    private string _description = "TestDescription";
    private List<ResourceAmountDto> _resourceAmounts = Resource.All.Select(r =>
    {
        // Simple distribution: each resource receives 10, except CO2 which receives 0
        var amount = r.Code == Resource.Co2.Code ? 0 : 10;
        return new ResourceAmountDto(r.Code, amount);
    }).ToList();

    /// <summary>
    ///     Initializes a new instance of the <see cref="CreateFactionCommandBuilder" /> class.
    /// </summary>
    /// <param name="presenter">The test presenter to be used in the command.</param>
    public CreateFactionCommandBuilder(TestPresenter<CreateFactionResponse> presenter)
    {
        _presenter = presenter;
    }

    /// <summary>
    ///     Sets the faction code to use in the command.
    /// </summary>
    /// <param name="code">The faction code.</param>
    /// <returns>The current <see cref="CreateFactionCommandBuilder" /> instance.</returns>
    public CreateFactionCommandBuilder WithCode(string code)
    {
        _code = code;
        return this;
    }

    /// <summary>
    ///     Sets the faction name to use in the command.
    /// </summary>
    /// <param name="name">The faction name.</param>
    /// <returns>The current <see cref="CreateFactionCommandBuilder" /> instance.</returns>
    public CreateFactionCommandBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    /// <summary>
    ///     Sets the faction description to use in the command.
    /// </summary>
    /// <param name="description">The faction description.</param>
    /// <returns>The current <see cref="CreateFactionCommandBuilder" /> instance.</returns>
    public CreateFactionCommandBuilder WithDescription(string description)
    {
        _description = description;
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
        var existing = _resourceAmounts.FirstOrDefault(r => r.ResourceCode == resource.Code);
        if (existing != null)
            existing.Amount = amount;
        else
            _resourceAmounts.Add(new ResourceAmountDto(resource.Code, amount));

        return this;
    }

    /// <summary>
    ///     Replaces the full list of resource amounts to be used in the command.
    /// </summary>
    /// <param name="amounts">A list of <see cref="ResourceAmountDto" /> representing the resource allocations.</param>
    /// <returns>The current <see cref="CreateFactionCommandBuilder" /> instance.</returns>
    public CreateFactionCommandBuilder WithResourceAmounts(List<ResourceAmountDto> amounts)
    {
        _resourceAmounts = amounts;
        return this;
    }

    /// <summary>
    ///     Builds a <see cref="CreateFactionCommand" /> instance with the configured values.
    /// </summary>
    /// <returns>A new <see cref="CreateFactionCommand" />.</returns>
    public CreateFactionCommand Build()
    {
        return new CreateFactionCommand(_code, _name, _description, _resourceAmounts, _presenter);
    }
}