namespace Business.Domain.Validators
{
    /// <summary>
    /// Represents a base class for validating any type of object.
    /// </summary>
    /// <typeparam name="T">The type of object to validate.</typeparam>
    public abstract class Validator<T>
    {
        /// <summary>
        /// Validates the specified object.
        /// </summary>
        /// <param name="obj">The object to validate.</param>
        /// <returns>A <see cref="ValidationResult"/> containing validation results, including any errors.</returns>
        public ValidationResult Validate(T obj)
        {
            var result = new ValidationResult();
            ValidateObject(obj, result);
            return result;
        }

        /// <summary>
        /// Performs object-specific validation.
        /// </summary>
        /// <param name="obj">The object to validate.</param>
        /// <param name="result">The <see cref="ValidationResult"/> to populate with validation errors.</param>
        protected abstract void ValidateObject(T obj, ValidationResult result);
    }
}
