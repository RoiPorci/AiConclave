﻿using System.Collections.Generic;

namespace Business.Application;
/// <summary>
/// Represents the base class for responses generated by use cases, 
/// providing a standard mechanism to capture errors.
/// </summary>
public abstract class UseCaseResponse
{
    /// <summary>
    /// Gets or sets the list of errors encountered during the operation.
    /// </summary>
    /// <remarks>
    /// This property will contain error messages if the operation fails.
    /// </remarks>
    public List<string> Errors { get; set; } = new List<string>();
}
