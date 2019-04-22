using System.Collections.Generic;
using System.Linq;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Class LogFilter.
    /// </summary>
    public class LogFilter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogFilter"/> class.
        /// </summary>
        public LogFilter()
        {
            ObjectEventTypes = new LinkedList<ObjectEventTypes>();
        }

        /// <summary>
        /// Gets or sets the type of the event.
        /// </summary>
        /// <value>The type of the event.</value>
        public string EventType { get; set; }

        /// <summary>
        /// Gets or sets the initiator identifier.
        /// </summary>
        /// <value>The initiator identifier.</value>
        public long? InitiatorId { get; set; }

        /// <summary>
        /// Gets or sets the limit.
        /// </summary>
        /// <value>The limit.</value>
        /// <remarks>Default is 30, and the maximum is 100.</remarks>
        public int? Limit { get; set; }

        /// <summary>
        /// Gets the object event types.
        /// </summary>
        /// <value>The object event types.</value>
        /// <remarks>An alternative way to filter by multiple object and event types.
        /// When this parameter is specified the <see cref="ObjectId"/>, <see cref="EventType"/> and <see cref="ObjectId"/> parameters are ignored.</remarks>
        public ICollection<ObjectEventTypes> ObjectEventTypes { get; }

        /// <summary>
        /// Gets or sets the object identifier.
        /// </summary>
        /// <value>The object identifier.</value>
        public long? ObjectId { get; set; }

        /// <summary>
        /// Gets or sets the type of the object.
        /// </summary>
        /// <value>The type of the object.</value>
        public string ObjectType { get; set; }

        /// <summary>
        /// Gets or sets the offset.
        /// </summary>
        /// <value>The offset.</value>
        public int? Offset { get; set; }

        /// <summary>
        /// Gets or sets the parent item identifier.
        /// </summary>
        /// <value>The parent item identifier.</value>
        public long? ParentItemId { get; set; }

        /// <summary>
        /// Gets or sets the parent project identifier.
        /// </summary>
        /// <value>The parent project identifier.</value>
        public long? ParentProjectId { get; set; }

        /// <summary>
        /// Gets or sets the page.
        /// </summary>
        /// <value>
        /// The page.
        /// </value>
        public long? Page { get; set; }

        // ReSharper disable once FunctionComplexityOverflow
        internal ICollection<KeyValuePair<string, string>> ToParameters()
        {
            LinkedList<KeyValuePair<string, string>> parameters = new LinkedList<KeyValuePair<string, string>>();

            if (!string.IsNullOrEmpty(ObjectType))
            {
                parameters.AddLast(new KeyValuePair<string, string>("object_type", ObjectType));
            }

            if (ObjectId.HasValue)
            {
                parameters.AddLast(new KeyValuePair<string, string>("object_id", ObjectId.Value.ToString()));
            }

            if (!string.IsNullOrEmpty(EventType))
            {
                parameters.AddLast(new KeyValuePair<string, string>("event_type", EventType));
            }

            if (ObjectEventTypes.Any())
            {
                parameters.AddLast(
                    new KeyValuePair<string, string>("object_event_types", $"[{string.Join(",", ObjectEventTypes)}]"));
            }

            if (ParentProjectId.HasValue)
            {
                parameters.AddLast(
                    new KeyValuePair<string, string>("parent_project_id", ParentProjectId.Value.ToString()));
            }

            if (ParentItemId.HasValue)
            {
                parameters.AddLast(new KeyValuePair<string, string>("parent_item_id", ParentItemId.Value.ToString()));
            }

            if (InitiatorId.HasValue)
            {
                parameters.AddLast(new KeyValuePair<string, string>("initiator_id", InitiatorId.Value.ToString()));
            }

            if (Page.HasValue)
            {
                parameters.AddLast(new KeyValuePair<string, string>("page", Page.Value.ToString()));
            }

            if (Limit.HasValue)
            {
                parameters.AddLast(new KeyValuePair<string, string>("limit", Limit.Value.ToString()));
            }

            if (Offset.HasValue)
            {
                parameters.AddLast(new KeyValuePair<string, string>("offset", Offset.Value.ToString()));
            }

            return parameters;
        }
    }
}
