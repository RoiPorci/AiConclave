using System.Collections.Generic;

namespace AiConclave.Business.Application;

/// <summary>
/// Base class for defining and validating sort options used in queries.
/// </summary>
public abstract class BaseSortOptions
{
    /// <summary>
    /// Gets the default sort key to use when none is provided.
    /// </summary>
    public abstract string DefaultSortBy { get; }

    /// <summary>
    /// Gets the set of valid sort keys accepted by the system.
    /// </summary>
    protected abstract HashSet<string> ValidSortByOptions { get; }

    /// <summary>
    /// Represents the ascending sort direction.
    /// </summary>
    public const string Ascending = "asc";

    /// <summary>
    /// Represents the descending sort direction.
    /// </summary>
    public const string Descending = "desc";

    private static readonly HashSet<string> ValidSortOrderOptions =
    [
        Ascending,
        Descending
    ];

    /// <summary>
    /// The default sort order used when none is specified.
    /// </summary>
    public const string DefaultSortOrder = Descending;

    /// <summary>
    /// Returns valid sort options based on the input.
    /// If inputs are <see langword="null"/> or invalid, defaults are used.
    /// </summary>
    /// <param name="sortBy">The desired sort field (e.g., "name", "code").</param>
    /// <param name="sortOrder">The desired sort order ("asc" or "desc").</param>
    /// <returns>A tuple containing the validated sort field and sort order.</returns>
    public (string SortBy, string SortOrder) GetValidSortOptions(string? sortBy, string? sortOrder)
    {
        // Validate and normalize the sortBy option
        var validSortBy = string.IsNullOrWhiteSpace(sortBy) || !ValidSortByOptions.Contains(Normalize(sortBy))
            ? DefaultSortBy
            : Normalize(sortBy);

        // Validate and normalize the sortOrder option
        var validSortOrder = string.IsNullOrWhiteSpace(sortOrder) || !ValidSortOrderOptions.Contains(Normalize(sortOrder))
            ? DefaultSortOrder
            : Normalize(sortOrder);

        return (validSortBy, validSortOrder);
    }

    /// <summary>
    /// Normalizes a given value to lowercase for consistent validation.
    /// </summary>
    /// <param name="value">The value to normalize.</param>
    /// <returns>A lowercase version of the input string.</returns>
    private static string Normalize(string value)
    {
        return value.ToLower();
    }
}
