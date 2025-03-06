using System.Collections.Generic;

namespace Business.Domain.Model
{
    /// <summary>
    /// Represents a resource in the system with a unique code, name, and description.
    /// </summary>
    public class Resource : ValueObject
    {
        /// <summary>
        /// Gets the unique code identifying the resource.
        /// </summary>
        public string Code { get; }

        /// <summary>
        /// Gets the name of the resource.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the description of the resource.
        /// </summary>
        public string Description { get; }

        /// <summary>Research & Development resource code.</summary>
        public const string RESEARCH_CODE = "RND";

        /// <summary>Energy resource code.</summary>
        public const string ENERGY_CODE = "NRG";

        /// <summary>Materials/Resources resource code.</summary>
        public const string MATERIALS_CODE = "RES";

        /// <summary>Economy resource code.</summary>
        public const string ECONOMY_CODE = "ECO";

        /// <summary>Social and political stability resource code.</summary>
        public const string STABILITY_CODE = "STA";

        /// <summary>Governance resource code.</summary>
        public const string GOVERNANCE_CODE = "GOV";

        /// <summary>
        /// Initializes a new instance of the <see cref="Resource"/> class.
        /// </summary>
        /// <param name="code">The unique resource code.</param>
        /// <param name="name">The name of the resource.</param>
        /// <param name="description">A brief description of the resource.</param>
        private Resource(string code, string name, string description)
        {
            Code = code;
            Name = name;
            Description = description;
        }

        /// <inheritdoc/>
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Code; // Equality is based on the Code property
        }

        /// <summary>
        /// Creates a new instance of <see cref="Resource"/>.
        /// </summary>
        /// <param name="code">The unique resource code.</param>
        /// <param name="name">The name of the resource.</param>
        /// <param name="description">A brief description of the resource.</param>
        /// <returns>A new instance of <see cref="Resource"/>.</returns>
        internal static Resource Create(string code, string name, string description) 
            => new Resource(code, name, description);

        /// <summary>
        /// Retrieves a <see cref="Resource"/> by its code.
        /// </summary>
        /// <param name="code">The unique code of the resource.</param>
        /// <returns>
        /// A <see cref="Result{T}"/> containing the <see cref="Resource"/> if found,
        /// or an error message if the code is invalid.
        /// </returns>
        public static Result<Resource> GetByCode(string code)
        {
            return !ResourceRegistry.Resources.TryGetValue(code, out var resource) 
                ? Result<Resource>.Failure($"Invalid resource code: {code}") 
                : Result<Resource>.Success(resource);
        }

        /// <summary>
        /// Determines whether a given resource code is valid.
        /// </summary>
        /// <param name="code">The resource code to check.</param>
        /// <returns><see langword="true"/> if the code exists; otherwise, <see langword="false"/>.</returns>
        public static bool IsValidCode(string code) => ResourceRegistry.Resources.ContainsKey(code);

        /// <summary>
        /// Gets all available resources.
        /// </summary>
        /// <returns>A read-only collection of all <see cref="Resource"/> instances.</returns>
        public static IReadOnlyCollection<Resource> GetAll() => ResourceRegistry.Resources.Values;

        /// <summary>
        /// Gets all available resource codes.
        /// </summary>
        /// <returns>A read-only collection of all resource codes.</returns>
        public static IReadOnlyCollection<string> GetAllCodes() => ResourceRegistry.Resources.Keys;

        /// <summary>
        /// Returns the string representation of the resource, which is its code.
        /// </summary>
        /// <returns>The resource code as a string.</returns>
        public override string ToString() => Code;
    }
}
