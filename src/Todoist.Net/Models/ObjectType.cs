namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a Todoist object type.
    /// </summary>
    /// <seealso cref="Todoist.Net.Models.StringEnum" />
    public class ObjectType : StringEnum
    {
        internal ObjectType(string value)
            : base(value)
        {
        }

        /// <summary>
        /// Gets the item type.
        /// </summary>
        /// <value>
        /// The item type.
        /// </value>
        public static ObjectType Item { get; } = new ObjectType("item");

        /// <summary>
        /// Gets the project type.
        /// </summary>
        /// <value>
        /// The project type.
        /// </value>
        public static ObjectType Project { get; } = new ObjectType("project");

        /// <summary>
        /// Gets the project comments type.
        /// </summary>
        /// <value>
        /// The project comments type.
        /// </value>
        public static ObjectType ProjectComments { get; } = new ObjectType("project_comments");
    }
}
