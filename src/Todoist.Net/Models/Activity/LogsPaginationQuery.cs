using System;
using System.Collections.Generic;
using System.Linq;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a paginated query for activity logs with common pagination parameters and a project filter.
    /// </summary>
    public class LogsPaginationQuery : PaginationQuery
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogsPaginationQuery"/> class.
        /// </summary>
        public LogsPaginationQuery()
        {
            ObjectEventTypes = new List<ObjectEventTypes>();
        }

        /// <summary>
        /// Gets the object event types.
        /// </summary>
        /// <remarks>
        /// An alternative way to filter by multiple object and event types.
        /// When this parameter is specified the <see cref="ObjectId"/>, <see cref="EventType"/> and <see cref="ObjectType"/> parameters are ignored.
        /// </remarks>
        public ICollection<ObjectEventTypes> ObjectEventTypes { get; }

        /// <summary>
        /// Gets or sets the type of the event.
        /// </summary>
        public LogEventType EventType { get; set; }

        /// <summary>
        /// Gets or sets the type of the object to filter activities by.
        /// </summary>
        public LogObjectType ObjectType { get; set; }

        /// <summary>
        /// Gets or sets the object identifier.
        /// </summary>
        public string ObjectId { get; set; }

        /// <summary>
        /// Gets or sets the parent project identifier.
        /// </summary>
        public string ParentProjectId { get; set; }

        /// <summary>
        /// Gets or sets the parent item identifier.
        /// </summary>
        public string ParentItemId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to include the parent object.
        /// </summary>
        /// <value><c>true</c> if the parent object should be included; otherwise, <c>false</c>.</value>
        public bool IncludeParentObject { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to include the child objects.
        /// </summary>
        /// <value><c>true</c> if the child objects should be included; otherwise, <c>false</c>.</value>
        public bool IncludeChildObjects { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the initiator ID is null.
        /// </summary>
        /// <value><c>true</c> if the initiator ID is null; otherwise, <c>false</c>.</value>
        public bool? InitiatorIdNull { get; set; }

        /// <summary>
        /// Gets or sets the initiator identifier.
        /// </summary>
        public long? InitiatorId { get; set; }

        /// <summary>
        /// Gets or sets the workspace identifier.
        /// </summary>
        public long? WorkspaceId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to annotate notes.
        /// </summary>
        /// <remarks>
        /// When <c>true</c>, includes additional information about comments in the 
        /// <c>extra_data</c> field, such as the content of the comment.
        /// </remarks>
        /// <value><c>true</c> if notes should be annotated; otherwise, <c>false</c>.</value>
        public bool AnnotateNotes { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to annotate parents.
        /// </summary>
        /// <remarks>
        /// When <c>true</c>, includes additional information about parent objects in the 
        /// <c>extra_data</c> field, such as the name of the parent project or task.
        /// </remarks>
        /// <value><c>true</c> if parents should be annotated; otherwise, <c>false</c>.</value>
        public bool AnnotateParents { get; set; }

        /// <summary>
        /// Gets or sets the date from which to start retrieving activity logs.
        /// </summary>
        public DateTime? DateFrom { get; set; }

        /// <summary>
        /// Gets or sets the date until which to retrieve activity logs.
        /// </summary>
        public DateTime? DateTo { get; set; }


        internal override Dictionary<string, string> ToParameters()
        {
            var objectEventTypesValue = ObjectEventTypes.Any() ? $"[{string.Join(",", ObjectEventTypes)}]" : null;

            var parameters = new Dictionary<string, string>(base.ToParameters())
            {
                { "object_event_types", objectEventTypesValue },
                { "event_type", EventType?.ToString() },
                { "object_type", ObjectType?.ToString() },
                { "object_id", ObjectId },
                { "parent_project_id", ParentProjectId },
                { "parent_item_id", ParentItemId },
                { "include_parent_object", IncludeParentObject ? "true" : null },
                { "include_child_objects", IncludeChildObjects ? "true" : null },
                { "initiator_id_null", InitiatorIdNull?.ToString().ToLower() },
                { "initiator_id", InitiatorId?.ToString() },
                { "workspace_id", WorkspaceId?.ToString() },
                { "annotate_notes", AnnotateNotes ? "true" : null },
                { "annotate_parents", AnnotateParents ? "true" : null },
                { "date_from", DateFrom?.ToString("o") },
                { "date_to", DateTo?.ToString("o") }
            };

            return parameters.ToNonEmptyValuesDictionary();
        }
    }
}
