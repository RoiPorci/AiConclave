using System.Threading.Tasks;

namespace AiConclave.Business.Domain.Specifications;

/// <summary>
///     Defines a validation rule for an entity of type <typeparamref name="T" />.
/// </summary>
/// <typeparam name="T">The type of entity to validate.</typeparam>
public interface ISpecification<in T>
{
    /// <summary>
    ///     Validates the given entity against the specification asynchronously.
    /// </summary>
    /// <param name="entity">The entity to validate.</param>
    /// <returns>
    ///     A <see cref="ValidationResult" /> containing any validation errors.
    /// </returns>
    Task<ValidationResult> ValidateAsync(T entity);
}