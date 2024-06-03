using System.Collections.Generic;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents an entity that could have its properties unset.
    /// </summary>
    /// <remarks>
    /// Unset properties are meant to be properties that are included
    /// in the outgoing requests although not being set to any value (<see langword="null"/>).
    /// </remarks>
    public interface IUnsettableProperties
    {
        /// <summary>
        /// Gets a collection of information about properties that are not set to any value (<see langword="null"/>)
        /// but still should be included in the outgoing requests.
        /// </summary>
        [JsonIgnore]
        HashSet<PropertyInfo> UnsetProperties { get; }
    }
}
