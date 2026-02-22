namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents workspace default access level values.
    /// </summary>
    public class WorkspaceDefaultAccessLevel : StringEnum
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WorkspaceDefaultAccessLevel"/> class.
        /// </summary>
        /// <param name="value">The underlying API value.</param>
        private WorkspaceDefaultAccessLevel(string value) : base(value) { }

        /// <summary>Gets restricted.</summary>
        public static WorkspaceDefaultAccessLevel Restricted { get; } = new WorkspaceDefaultAccessLevel("restricted");
        
        /// <summary>Gets team.</summary>
        public static WorkspaceDefaultAccessLevel Team { get; } = new WorkspaceDefaultAccessLevel("team");
    }
}