using AiConclave.Business.Application.Factions.OwnedResources;
using AiConclave.Business.Domain.Model;

namespace AiConclave.Business.Tests.InitFactionResourcesTests.Helpers;

/// <summary>
///     Builder class for creating instances of <see cref="InitFactionResourcesCommand" />.
///     Provides methods to customize command properties for testing.
/// </summary>
public class InitFactionResourcesCommandBuilder
{
    private readonly TestPresenter<InitFactionResourcesResponse> _presenter;
    private Guid _factionId = Guid.NewGuid();

    private List<ResourceAmountDto> _resourceAmounts = Resource.All.Select(r =>
    {
        // Simple distribution: each resource receives 10, except CO2 which receives 0
        var amount = r.Code == Resource.Co2.Code ? 0 : 10;
        return new ResourceAmountDto(r.Code, amount);
    }).ToList();

    /// <summary>
    ///     Initializes a new instance of the <see cref="InitFactionResourcesCommandBuilder" /> class.
    /// </summary>
    /// <param name="presenter">The test presenter to be used in the command.</param>
    public InitFactionResourcesCommandBuilder(TestPresenter<InitFactionResourcesResponse> presenter)
    {
        _presenter = presenter;
    }

    /// <summary>
    ///     Sets the faction ID to use in the command.
    /// </summary>
    /// <param name="id">The faction identifier.</param>
    /// <returns>The current <see cref="InitFactionResourcesCommandBuilder" /> instance.</returns>
    public InitFactionResourcesCommandBuilder WithFactionId(Guid id)
    {
        _factionId = id;
        return this;
    }

    /// <summary>
    ///     Sets or updates the amount for a specific resource in the command.
    /// </summary>
    /// <param name="resource">The resource to set or update.</param>
    /// <param name="amount">The amount to assign to the resource.</param>
    /// <returns>The current <see cref="InitFactionResourcesCommandBuilder" /> instance.</returns>
    public InitFactionResourcesCommandBuilder WithResource(Resource resource, int amount)
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
    /// <returns>The current <see cref="InitFactionResourcesCommandBuilder" /> instance.</returns>
    public InitFactionResourcesCommandBuilder WithResourceAmounts(List<ResourceAmountDto> amounts)
    {
        _resourceAmounts = amounts;
        return this;
    }

    /// <summary>
    ///     Builds a <see cref="InitFactionResourcesCommand" /> instance with the configured values.
    /// </summary>
    /// <returns>A new <see cref="InitFactionResourcesCommand" />.</returns>
    public InitFactionResourcesCommand Build()
    {
        return new InitFactionResourcesCommand(_factionId, _resourceAmounts, _presenter);
    }
}