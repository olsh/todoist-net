using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents view options.
    /// </summary>
    public class ViewOptions : BaseViewOptions, IWithRelationsArgument
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewOptions"/> class.
        /// </summary>
        /// <param name="objectId">The object ID.</param>
        /// <param name="viewType">The view type.</param>
        public ViewOptions(ComplexId objectId, ViewOptionsType viewType)
        {
            ObjectId = objectId;
            ViewType = viewType;
        }

        [JsonConstructor]
        internal ViewOptions()
        {
        }

        /// <summary>
        /// Gets or sets the object ID.
        /// </summary>
        [JsonPropertyName("object_id")]
        public ComplexId ObjectId { get; set; }

        /// <summary>
        /// Gets or sets the view type.
        /// </summary>
        [JsonPropertyName("view_type")]
        public ViewOptionsType ViewType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance of view options is deleted.
        /// </summary>
        [JsonPropertyName("is_deleted")]
        public bool? IsDeleted { get; set; }
        

        /// <summary>
        /// Updates the related temporary ids.
        /// </summary>
        /// <param name="map">The map.</param>
        void IWithRelationsArgument.UpdateRelatedTempIds(IDictionary<Guid, string> map)
        {
            if (map.TryGetValue(ObjectId.TempId, out var persistentObjectId))
            {
                ObjectId = new ComplexId(persistentObjectId);
            }
        }
    }
}
