using System.Collections.Generic;

namespace AiConclave.Business.Application.Factions;

public class ListFactionsSortOptions : BaseSortOptions
{
    public const string Name = "name";
    
    public const string Code = "code";
    
    public override string DefaultSortBy => Code;
    
    protected override HashSet<string> ValidSortByOptions =>
    [
        Name,
        Code
    ];
}