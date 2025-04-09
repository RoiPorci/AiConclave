using System;

namespace AiConclave.Business.Application.Factions;

public class ModifyFactionInfosCommand : IRequestWithPresenter<ModifyFactionInfosResponse>
{
    public ModifyFactionInfosCommand(Guid factionId, string? code, string? name, string? description, IResponsePresenter<ModifyFactionInfosResponse> presenter)
    {
        Presenter = presenter;
        FactionId = factionId;
        Code = code;
        Name = name;
        Description = description;
    }

    /// <summary>
    ///     Gets or sets the unique identifier of the modified faction.
    /// </summary>
    public Guid FactionId { get; set; }
    
    /// <summary>
    ///     Gets or sets the unique code of the faction.
    /// </summary>
    public string? Code { get; set; }
    
    /// <summary>
    ///     Gets or sets the name of the faction.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    ///     Gets or sets the description of the faction.
    /// </summary>
    public string? Description { get; set; }

    public IResponsePresenter<ModifyFactionInfosResponse> Presenter { get; }
}