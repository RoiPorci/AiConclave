﻿using System.Collections.Generic;

namespace AiConclave.Business.Domain.Specifications;

/// <summary>
///     Represents the result of a validation operation, including a list of errors.
/// </summary>
public class ValidationResult
{
    /// <summary>
    ///     Gets the list of validation errors.
    /// </summary>
    public List<string> Errors { get; } = [];

    /// <summary>
    ///     Indicates whether the validation succeeded.
    /// </summary>
    public bool IsValid => Errors.Count == 0;

    /// <summary>
    ///     Adds an error to the validation result.
    /// </summary>
    /// <param name="error">The error message to add.</param>
    public void AddError(string error)
    {
        Errors.Add(error);
    }
}