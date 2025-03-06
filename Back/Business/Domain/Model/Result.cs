using System.Collections.Generic;

namespace Business.Domain.Model
{
    /// <summary>
    /// Represents the result of an operation, encapsulating success or failure state, a value, and any associated errors.
    /// </summary>
    /// <typeparam name="T">The type of the value encapsulated by the result.</typeparam>
    public class Result<T> where T : class
    {
        /// <summary>
        /// Gets and sets a value indicating whether the operation was successful.
        /// </summary>
        public bool IsSuccess => Errors.Count == 0;

        /// <summary>
        /// Gets the value produced by the operation if successful; otherwise, <see langword="null"/>.
        /// </summary>
        public T? Value { get; private set; }

        /// <summary>
        /// Gets the list of error messages associated with a failed operation.
        /// </summary>
        public List<string> Errors { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Result{T}"/> class.
        /// </summary>
        /// <param name="value">The value produced by the operation, or <see langword="null"/> for a failure.</param>
        /// <param name="errors">The list of error messages, or <see langword="null"/> for a success.</param>
        private Result(T? value, List<string>? errors)
        {
            Value = value;
            Errors = errors ?? new List<string>();
        }

        /// <summary>
        /// Creates a successful <see cref="Result{T}"/> instance with the specified value.
        /// </summary>
        /// <param name="value">The value produced by the operation.</param>
        /// <returns>A <see cref="Result{T}"/> representing a successful operation.</returns>
        public static Result<T> Success(T value)
        {
            return new Result<T>(value, null);
        }

        /// <summary>
        /// Creates a failed <see cref="Result{T}"/> instance with the specified error messages.
        /// </summary>
        /// <param name="errors">A list of error messages describing the failure.</param>
        /// <returns>A <see cref="Result{T}"/> representing a failed operation.</returns>
        public static Result<T> Failure(List<string> errors)
        {
            return new Result<T>(null, errors);
        }

        /// <summary>
        /// Creates a failed <see cref="Result{T}"/> instance with the specified error messages.
        /// </summary>
        /// <param name="error">An error messages describing the failure.</param>
        /// <returns>A <see cref="Result{T}"/> representing a failed operation.</returns>
        public static Result<T> Failure(string error)
        {
            return new Result<T>(null, [error]);
        }
    }
}