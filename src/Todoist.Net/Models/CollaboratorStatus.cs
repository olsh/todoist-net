namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents the status of a collaborator.
    /// </summary>
    /// <seealso cref="Todoist.Net.Models.StringEnum" />
    public class CollaboratorStatus : StringEnum
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CollaboratorStatus" /> class.
        /// </summary>
        /// <param name="value">The value.</param>
        private CollaboratorStatus(string value)
            : base(value)
        {
        }

        /// <summary>
        /// Gets the active status.
        /// </summary>
        /// <value>The active status.</value>
        public static CollaboratorStatus Active { get; } = new CollaboratorStatus("active");

        /// <summary>
        /// Gets the invited status.
        /// </summary>
        /// <value>The invited status.</value>
        public static CollaboratorStatus Invited { get; } = new CollaboratorStatus("invited");

    }
}
