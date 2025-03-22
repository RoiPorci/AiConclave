using System.Collections.Generic;
using System.Linq;

namespace AiConclave.Business.Domain.Model;

/// <summary>
///     Represents a resource using the Enum Object pattern.
/// </summary>
/// <remarks>
///     This class mimics an enumeration while allowing richer object behavior.
///     Resources are predefined and identified by a unique <see cref="Code" />.
/// </remarks>
public class Resource
{
    /// <summary>
    ///     Research and Development resource.
    /// </summary>
    public static readonly Resource Research = new("RND", "Research & Development",
        "Innovation and technology progress.", false);

    /// <summary>
    ///     Energy resource.
    /// </summary>
    public static readonly Resource Energy = new("NRG", "Energy", "Power production and distribution.", false);

    /// <summary>
    ///     Materials resource.
    /// </summary>
    public static readonly Resource Materials = new("RES", "Materials", "Raw materials and resources.", false);

    /// <summary>
    ///     Economy resource.
    /// </summary>
    public static readonly Resource Economy = new("ECO", "Economy", "Financial and trade stability.", false);

    /// <summary>
    ///     Stability resource.
    /// </summary>
    public static readonly Resource Stability = new("STA", "Stability", "Social and political stability.", false);

    /// <summary>
    ///     Governance resource.
    /// </summary>
    public static readonly Resource Governance = new("GOV", "Governance", "Government and administration.", false);

    /// <summary>
    ///     Carbon emissions resource (can be negative).
    /// </summary>
    public static readonly Resource Co2 = new("Co2", "Carbon Emissions", "Pollution emitted", true);

    /// <summary>
    ///     Gets a read-only collection of all available resources.
    /// </summary>
    public static readonly IReadOnlyCollection<Resource> All =
    [
        Research, Energy, Materials, Economy, Stability, Governance, Co2
    ];

    private Resource(string code, string name, string description, bool allowsNegativeValues)
    {
        Code = code;
        Name = name;
        Description = description;
        AllowsNegativeValues = allowsNegativeValues;
    }

    /// <summary>
    ///     Gets the unique code of the resource.
    /// </summary>
    public string Code { get; }

    /// <summary>
    ///     Gets the name of the resource.
    /// </summary>
    public string Name { get; }

    /// <summary>
    ///     Gets the description of the resource.
    /// </summary>
    public string Description { get; }

    /// <summary>
    ///     Gets a value indicating whether the resource allows negative values.
    /// </summary>
    public bool AllowsNegativeValues { get; }

    /// <summary>
    ///     Retrieves a resource by its code.
    /// </summary>
    /// <param name="code">The code of the resource.</param>
    /// <returns>The corresponding <see cref="Resource" /> if found; otherwise, <see langword="null" />.</returns>
    public static Resource? FromCode(string code)
    {
        return All.FirstOrDefault(r => r.Code == code);
    }

    /// <summary>
    ///     Determines whether the specified code is a valid resource code.
    /// </summary>
    /// <param name="code">The code to validate.</param>
    /// <returns><see langword="true" /> if the code is valid; otherwise, <see langword="false" />.</returns>
    public static bool IsValidCode(string code)
    {
        return All.Any(r => r.Code == code);
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return Code;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is Resource other && Code == other.Code;
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return Code.GetHashCode();
    }
}