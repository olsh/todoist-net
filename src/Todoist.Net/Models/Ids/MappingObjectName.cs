using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a Todoist id mapping object name.
    /// </summary>
    /// <seealso cref="Todoist.Net.Models.StringEnum" />
    public class MappingObjectName : StringEnum
    {
        [JsonConstructor]
        internal MappingObjectName(string value)
            : base(value)
        {
        }

        /// <summary>
        /// Gets the tasks type.
        /// </summary>
        public static MappingObjectName Tasks { get; } = new MappingObjectName("tasks");

        /// <summary>
        /// Gets the sections type.
        /// </summary>
        public static MappingObjectName Sections { get; } = new MappingObjectName("sections");

        /// <summary>
        /// Gets the projects type.
        /// </summary>
        public static MappingObjectName Projects { get; } = new MappingObjectName("projects");

        /// <summary>
        /// Gets the comments type.
        /// </summary>
        public static MappingObjectName Comments { get; } = new MappingObjectName("comments");

        /// <summary>
        /// Gets the reminders type.
        /// </summary>
        public static MappingObjectName Reminders { get; } = new MappingObjectName("reminders");

        /// <summary>
        /// Gets the location reminders type.
        /// </summary>
        public static MappingObjectName LocationReminders { get; } = new MappingObjectName("location_reminders");
    }
}
