using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Newtonsoft.Json;

using Todoist.Net.Models.Types;
using Todoist.Net.Serialization.Converters;

namespace Todoist.Net.Models
{
    public class Note : BaseEntity, IWithRelationsArgument
    {
        public Note(string content)
        {
            Content = content;

            UidsToNotify = new Collection<int>();
        }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("file_attachment")]
        public FileAttachment FileAttachment { get; set; }

        [JsonProperty("is_archived")]
        [JsonConverter(typeof(BoolConverter))]
        public bool? IsArchived { get; internal set; }

        [JsonProperty("is_deleted")]
        [JsonConverter(typeof(BoolConverter))]
        public bool? IsDeleted { get; internal set; }

        [JsonProperty("item_id")]
        public ComplexId? ItemId { get; set; }

        [JsonProperty("posted")]
        public DateTime? Posted { get; internal set; }

        [JsonProperty("posted_uid")]
        public int? PostedUid { get; internal set; }

        [JsonProperty("project_id")]
        public ComplexId? ProjectId { get; internal set; }

        [JsonProperty("uids_to_notify")]
        public ICollection<int> UidsToNotify { get; internal set; }

        public void UpdateRelatedTempIds(IDictionary<Guid, int> map)
        {
            int persistentProjectId;
            if (ProjectId.HasValue && map.TryGetValue(ProjectId.Value.TempId, out persistentProjectId))
            {
                ProjectId = new ComplexId(persistentProjectId);
            }

            int persistentItemId;
            if (ItemId.HasValue && map.TryGetValue(ItemId.Value.TempId, out persistentItemId))
            {
                ItemId = new ComplexId(persistentItemId);
            }
        }
    }
}
