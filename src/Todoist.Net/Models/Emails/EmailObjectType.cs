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
        /// Gets the task type.
        /// </summary>
        /// <value>
        /// The task type.
        /// </value>
        public static EmailObjectType Task { get; } = new EmailObjectType("task");

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
