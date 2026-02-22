using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a Todoist log object type.
    /// </summary>
    /// <seealso cref="Todoist.Net.Models.StringEnum" />
    public class LogObjectType : StringEnum
    {
        [JsonConstructor]
        internal LogObjectType(string value)
            : base(value)
        {
        }

        /// <summary>
        /// Gets the item type.
        /// </summary>
        /// <remarks>
        /// Todoist API uses the legacy "item" string for tasks in sync endpoints.
        /// </remarks>
        /// <value>
        /// The item type.
        /// </value>
        public static LogObjectType Item { get; } = new LogObjectType("item");

        /// <summary>
        /// Gets the project type.
        /// </summary>
        /// <value>
        /// The project type.
        /// </value>
        public static LogObjectType Project { get; } = new LogObjectType("project");

        /// <summary>
        /// Gets the note type.
        /// </summary>
        /// <value>
        /// The note type.
        /// </value>
        public static LogObjectType Note { get; } = new LogObjectType("note");
    }
}
