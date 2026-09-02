namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents workspace creator role values.
    /// </summary>
    public class WorkspaceCreatorRole : StringEnum
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WorkspaceCreatorRole"/> class.
        /// </summary>
        /// <param name="value">The underlying API value.</param>
        private WorkspaceCreatorRole(string value) : base(value) { }

        /// <summary>Gets owner_founder.</summary>
        public static WorkspaceCreatorRole OwnerFounder { get; } = new WorkspaceCreatorRole("owner_founder");
        
        /// <summary>Gets leader.</summary>
        public static WorkspaceCreatorRole Leader { get; } = new WorkspaceCreatorRole("leader");
        
        /// <summary>Gets individual_contributor.</summary>
        public static WorkspaceCreatorRole IndividualContributor { get; } = new WorkspaceCreatorRole("individual_contributor");
    }
}