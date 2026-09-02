using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a Todoist log event type.
    /// </summary>
    /// <seealso cref="Todoist.Net.Models.StringEnum" />
    public class LogEventType : StringEnum
    {
        [JsonConstructor]
        internal LogEventType(string value)
            : base(value)
        {
        }

        /// <summary>Gets the added type.</summary>
        public static LogEventType Added { get; } = new LogEventType("added");

        /// <summary>Gets the deleted type.</summary>
        public static LogEventType Deleted { get; } = new LogEventType("deleted");

        /// <summary>Gets the updated type.</summary>
        public static LogEventType Updated { get; } = new LogEventType("updated");

        /// <summary>Gets the completed type.</summary>
        public static LogEventType Completed { get; } = new LogEventType("completed");

        /// <summary>Gets the uncompleted type.</summary>
        public static LogEventType Uncompleted { get; } = new LogEventType("uncompleted");

        /// <summary>Gets the archived type.</summary>
        public static LogEventType Archived { get; } = new LogEventType("archived");

        /// <summary>Gets the unarchived type.</summary>
        public static LogEventType Unarchived { get; } = new LogEventType("unarchived");

        /// <summary>Gets the shared type.</summary>
        public static LogEventType Shared { get; } = new LogEventType("shared");

        /// <summary>Gets the left type.</summary>
        public static LogEventType Left { get; } = new LogEventType("left");

        /// <summary>Gets the reordered type.</summary>
        public static LogEventType Reordered { get; } = new LogEventType("reordered");

        /// <summary>Gets the moved type.</summary>
        public static LogEventType Moved { get; } = new LogEventType("moved");
    }
}
