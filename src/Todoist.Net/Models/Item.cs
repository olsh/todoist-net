using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Newtonsoft.Json;

using Todoist.Net.Models.Types;
using Todoist.Net.Serialization.Converters;

namespace Todoist.Net.Models
{
    public class Item : BaseEntity, IWithRelationsArgument
    {
        public Item(string content)
        {
            Content = content;
            Labels = new Collection<int>();
        }

        [JsonProperty("assigned_by_uid")]
        public int? AssignedByUid { get; set; }

        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty("collapsed")]
        public bool? Collapsed { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("date_added")]
        public DateTime? DateAdded { get; internal set; }

        [JsonProperty("date_lang")]
        public string DateLang { get; set; }

        [JsonProperty("date_string")]
        public string DateString { get; set; }

        [JsonProperty("day_order")]
        public int? DayOrder { get; set; }

        [JsonProperty("due_date_utc")]
        public DateTime? DueDateUtc { get; set; }

        [JsonProperty("indent")]
        public int? Indent { get; set; }

        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty("in_history")]
        public bool? InHistory { get; internal set; }

        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty("is_archived")]
        public bool? IsArchived { get; internal set; }

        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty("checked")]
        public bool? IsChecked { get; internal set; }

        [JsonConverter(typeof(BoolConverter))]
        [JsonProperty("is_deleted")]
        public bool? IsDeleted { get; internal set; }

        [JsonProperty("item_order")]
        public int? ItemOrder { get; set; }

        [JsonProperty("labels")]
        public ICollection<int> Labels { get; internal set; }

        [JsonProperty("priority")]
        public int? Priority { get; set; }

        [JsonProperty("responsible_uid")]
        public int? ResponsibleUid { get; set; }

        [JsonProperty("sync_id")]
        public int? SyncId { get; internal set; }

        [JsonProperty("user_id")]
        public int? UserId { get; internal set; }

        [JsonProperty("project_id")]
        public ComplexId ProjectId { get; set; }

        public void UpdateRelatedTempIds(IDictionary<Guid, int> map)
        {
            int persistentProjectId;
            if (map.TryGetValue(ProjectId.TempId, out persistentProjectId))
            {
                ProjectId = new ComplexId(persistentProjectId);
            }
        }
    }
}
