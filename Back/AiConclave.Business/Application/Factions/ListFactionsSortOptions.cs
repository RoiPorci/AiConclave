using System.Collections.Generic;

namespace AiConclave.Business.Application.Factions;

/// <summary>
/// Defines the available sort options for listing factions.
/// </summary>
public class ListFactionsSortOptions : BaseSortOptions
{
    /// <summary>
    /// Sort key for sorting by faction name.
    /// </summary>
    public const string Name = "name";

    /// <summary>
    /// Sort key for sorting by faction code.
    /// </summary>
    public const string Code = "code";

    /// <summary>
    /// Gets the default sort key when none is specified.
    /// </summary>
    public override string DefaultSortBy => Code;

    /// <summary>
    /// Gets the set of valid sort keys that can be used in queries.
    /// </summary>
    protected override HashSet<string> ValidSortByOptions =>
    [
        Name,
        Code
    ];
}