using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a Todoist email object type.
    /// </summary>
    /// <seealso cref="Todoist.Net.Models.StringEnum" />
    public class EmailObjectType : StringEnum
    {
        [JsonConstructor]
        internal EmailObjectType(string value)
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
        public static EmailObjectType Item { get; } = new EmailObjectType("item");

        /// <summary>
        /// Gets the project type.
        /// </summary>
        /// <value>
        /// The project type.
        /// </value>
        public static EmailObjectType Project { get; } = new EmailObjectType("project");

        /// <summary>
        /// Gets the project comments type.
        /// </summary>
        /// <value>
        /// The project comments type.
        /// </value>
        public static EmailObjectType ProjectComments { get; } = new EmailObjectType("project_comments");
    }
}
