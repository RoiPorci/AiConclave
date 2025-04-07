using System.Collections.Generic;

namespace AiConclave.Business.Application;

public abstract class BaseSortOptions
{
    public abstract string DefaultSortBy { get; }
    protected abstract HashSet<string> ValidSortByOptions { get; }   
    
    
    public const string Ascending = "asc";
        
    public const string Descending = "desc";
        
    private static readonly HashSet<string> ValidSortOrderOptions =
    [
        Ascending,
        Descending
    ];
        
    public const string DefaultSortOrder = Descending;
        
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