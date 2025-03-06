using System.Collections.Generic;

namespace Business.Domain.Model
{
    /// <summary>
    /// Registry of predefined resources available in the system.
    /// </summary>
    public static class ResourceRegistry
    {
        /// <summary>
        /// Represents the research and development resource.
        /// </summary>
        public static readonly Resource Research = Resource.Create(
            Resource.RESEARCH_CODE, 
            "Research & Development", 
            "Scientific and technological advancements."
        );

        /// <summary>
        /// Represents the energy resource.
        /// </summary>
        public static readonly Resource Energy = Resource.Create(
            Resource.ENERGY_CODE, 
            "Energy", 
            "Power sources such as electricity, fuel, and renewable energy."
        );

        /// <summary>
        /// Represents the materials' resource.
        /// </summary>
        public static readonly Resource Materials = Resource.Create(
            Resource.MATERIALS_CODE, 
            "Materials", 
            "Basic resources used for construction and production."
        );

        /// <summary>
        /// Represents the economy resource.
        /// </summary>
        public static readonly Resource Economy = Resource.Create(
            Resource.ECONOMY_CODE, 
            "Economy", 
            "Financial stability and trade efficiency."
        );

        /// <summary>
        /// Represents the stability resource.
        /// </summary>
        public static readonly Resource Stability = Resource.Create(
            Resource.STABILITY_CODE, 
            "Stability", 
            "Political and social stability."
        );

        /// <summary>
        /// Represents the governance resource.
        /// </summary>
        public static readonly Resource Governance = Resource.Create(
            Resource.GOVERNANCE_CODE, 
            "Governance", 
            "Control and decision-making processes."
        );

        /// <summary>
        /// Dictionary mapping resource codes to their corresponding <see cref="Resource"/> instances.
        /// </summary>
        public static readonly Dictionary<string, Resource> Resources = new()
        {
            { Resource.RESEARCH_CODE, Research },
            { Resource.ENERGY_CODE, Energy },
            { Resource.MATERIALS_CODE, Materials },
            { Resource.ECONOMY_CODE, Economy },
            { Resource.STABILITY_CODE, Stability },
            { Resource.GOVERNANCE_CODE, Governance }
        };
    }
}
