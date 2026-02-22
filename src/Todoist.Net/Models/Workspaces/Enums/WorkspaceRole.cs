namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a workspace role.
    /// </summary>
    public class WorkspaceRole : StringEnum
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WorkspaceRole"/> class.
        /// </summary>
        /// <param name="value">The underlying API value.</param>
        private WorkspaceRole(string value)
            : base(value)
        {
        }

        /// <summary>
        /// Gets the admin workspace role.
        /// </summary>
        public static WorkspaceRole Admin { get; } = new WorkspaceRole("ADMIN");

        /// <summary>
        /// Gets the member workspace role.
        /// </summary>
        public static WorkspaceRole Member { get; } = new WorkspaceRole("MEMBER");

        /// <summary>
        /// Gets the guest workspace role.
        /// </summary>
        public static WorkspaceRole Guest { get; } = new WorkspaceRole("GUEST");
    }
}