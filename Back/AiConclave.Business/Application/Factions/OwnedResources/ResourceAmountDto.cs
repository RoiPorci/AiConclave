namespace AiConclave.Business.Application.Factions.OwnedResources;

/// <summary>
/// Data transfer object representing the amount of a specific resource.
/// </summary>
public class ResourceAmountDto
{
    /// <summary>
    /// Gets or sets the code of the resource.
    /// </summary>
    public string ResourceCode { get; set; }

    /// <summary>
    /// Gets or sets the amount associated with the resource.
    /// </summary>
    public int Amount { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ResourceAmountDto"/> class with the specified resource code and amount.
    /// </summary>
    /// <param name="resourceCode">The code identifying the resource.</param>
    /// <param name="amount">The amount of the resource.</param>
    public ResourceAmountDto(string resourceCode, int amount)
    {
        ResourceCode = resourceCode;
        Amount = amount;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ResourceAmountDto"/> class.
    /// Parameterless constructor required for serialization.
    /// </summary>
    public ResourceAmountDto() {}
}