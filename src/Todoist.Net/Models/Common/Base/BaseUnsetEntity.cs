using System.Collections.Generic;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a base entity that implements <see cref="IUnsettableProperties"/>.
    /// </summary>
    /// <seealso cref="IUnsettableProperties"/>
    public abstract class BaseUnsetEntity : BaseEntity, IUnsettableProperties
    {
        HashSet<PropertyInfo> IUnsettableProperties.UnsetProperties { get; } = new HashSet<PropertyInfo>();

        private protected BaseUnsetEntity(ComplexId id)
            : base(id)
        {
        }

        [JsonConstructor]
        private protected BaseUnsetEntity()
        {
        }
    }
}
