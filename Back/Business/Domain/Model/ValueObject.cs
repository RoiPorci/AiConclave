using System.Collections.Generic;
using System.Linq;

namespace Business.Domain.Model
{
    /// <summary>
    /// Represents a base class for value objects, providing equality comparison logic.
    /// </summary>
    public abstract class ValueObject
    {
        /// <summary>
        /// Gets the components that define equality for the value object.
        /// </summary>
        /// <returns>An enumerable of objects used for equality comparison.</returns>
        protected abstract IEnumerable<object> GetEqualityComponents();

        /// <summary>
        /// Determines whether this instance is equal to another object.
        /// </summary>
        /// <param name="obj">The object to compare with this instance.</param>
        /// <returns><see langword="true"/> if the objects are equal; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != GetType()) return false;

            var other = (ValueObject)obj;
            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A hash code based on the equality components.</returns>
        public override int GetHashCode() =>
            GetEqualityComponents()
                .Aggregate(17, (hash, component) =>
                    hash * 31 + (component?.GetHashCode() ?? 0));
        
        /// <summary>
        /// Determines whether two <see cref="ValueObject"/> instances are equal.
        /// </summary>
        /// <param name="left">The first <see cref="ValueObject"/> to compare.</param>
        /// <param name="right">The second <see cref="ValueObject"/> to compare.</param>
        /// <returns><see langword="true"/> if the instances are equal; otherwise, <see langword="false"/>.</returns>
        public static bool operator ==(ValueObject left, ValueObject? right) =>
            left?.Equals(right) ?? right is null;

        /// <summary>
        /// Determines whether two <see cref="ValueObject"/> instances are not equal.
        /// </summary>
        /// <param name="left">The first <see cref="ValueObject"/> to compare.</param>
        /// <param name="right">The second <see cref="ValueObject"/> to compare.</param>
        /// <returns><see langword="true"/> if the instances are not equal; otherwise, <see langword="false"/>.</returns>
        public static bool operator !=(ValueObject left, ValueObject right) => 
            !(left == right);
    }
}
