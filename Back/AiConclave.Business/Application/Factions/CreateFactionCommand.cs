using System;
using System.Collections.Generic;
using AiConclave.Business.Application.Factions.DTOs;

namespace AiConclave.Business.Application.Factions;

/// <summary>
///     Command model for creating a new faction.
/// </summary>
public class CreateFactionCommand : IRequestWithPresenter<CreateFactionResponse>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="CreateFactionCommand" /> class.
    /// </summary>
    /// <param name="code">The unique code of the faction.</param>
    /// <param name="name">The name of the faction.</param>
    /// <param name="description">The description of the faction.</param>
    /// <param name="resourceAmounts">The resource amounts for the faction.</param>
    /// <param name="presenter">The presenter responsible for handling the response.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="presenter" /> is <see langword="null" />.</exception>
    public CreateFactionCommand
    (
        string code,
        string name,
        string description,
        List<ResourceAmountDto> resourceAmounts,
        IResponsePresenter<CreateFactionResponse> presenter
    )
    {
        Code = code;
        Name = name;
        Description = description;
        ResourceAmounts = resourceAmounts;
        Presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
    }

    /// <summary>
    ///     Gets or sets the unique code of the faction.
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    ///     Gets or sets the name of the faction.
    /// </summary>
    public string Name { get; set;}

    /// <summary>
    ///     Gets or sets the description of the faction.
    /// </summary>
    public string Description { get; set;}

    /// <summary>
    ///     Gets or sets the resource amounts for the faction.
    /// </summary>
    public List<ResourceAmountDto> ResourceAmounts { get; set;}

    /// <summary>
    ///     Gets or sets the presenter responsible for handling the response.
    /// </summary>
    public IResponsePresenter<CreateFactionResponse> Presenter { get; }
}