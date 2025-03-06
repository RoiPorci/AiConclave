using System;
using System.Linq;
using Business.Domain.Model;

namespace Business.Domain.Entities
{
    /// <summary>
    /// Represents a resource owned by a faction, including its quantity and related information.
    /// </summary>
    public class OwnedResource
    {
        /// <summary>
        /// Gets or sets the unique identifier of the owned resource instance.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the amount of the resource owned.
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Gets or sets the faction that owns the resource.
        /// </summary>
        public Faction Owner { get; set; }

        /// <summary>
        /// Gets or sets the code of the resource.
        /// </summary>
        public string ResourceCode { get; set; }

        /// <summary>
        /// Gets the corresponding <see cref="Resource"/> based on <see cref="ResourceCode"/>.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown if the resource code is invalid.</exception>
        public Resource Resource => Resource.GetByCode(ResourceCode).IsSuccess 
            ? Resource.GetByCode(ResourceCode).Value! 
            : throw new ArgumentException(Resource.GetByCode(ResourceCode).Errors.FirstOrDefault());
        
        /// <summary>
        /// Initializes a new instance of the <see cref="OwnedResource"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the owned resource.</param>
        /// <param name="amount">The amount of the resource owned.</param>
        /// <param name="owner">The faction that owns the resource.</param>
        /// <param name="resourceCode">The code of the resource.</param>
        private OwnedResource(Guid id, int amount, Faction owner, string resourceCode)
        {
            Id = id;
            Amount = amount;
            Owner = owner;
            ResourceCode = resourceCode;
        }

        /// <summary>
        /// Creates a new instance of <see cref="OwnedResource"/> with a generated unique identifier.
        /// </summary>
        /// <param name="amount">The amount of the resource owned.</param>
        /// <param name="owner">The faction that owns the resource.</param>
        /// <param name="resourceCode">The code of the resource.</param>
        /// <returns>A new instance of <see cref="OwnedResource"/>.</returns>
        public static OwnedResource Create(int amount, Faction owner, string resourceCode)
        {
            return new OwnedResource(Guid.NewGuid(), amount, owner, resourceCode);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OwnedResource"/> class.
        /// This constructor is required for EF Core. It should never be used.
        /// </summary>
#pragma warning disable CS8618
        public OwnedResource() {}  
#pragma warning restore CS8618
    }
}