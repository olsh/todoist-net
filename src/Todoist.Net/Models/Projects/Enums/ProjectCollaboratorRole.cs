namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents project collaborator role values.
    /// </summary>
    public class ProjectCollaboratorRole : StringEnum
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectCollaboratorRole"/> class.
        /// </summary>
        /// <param name="value">The underlying API value.</param>
        private ProjectCollaboratorRole(string value) : base(value) { }

        /// <summary>Gets CREATOR.</summary>
        public static ProjectCollaboratorRole Creator { get; } = new ProjectCollaboratorRole("CREATOR");
        
        /// <summary>Gets ADMIN.</summary>
        public static ProjectCollaboratorRole Admin { get; } = new ProjectCollaboratorRole("ADMIN");
        
        /// <summary>Gets READ_WRITE.</summary>
        public static ProjectCollaboratorRole ReadWrite { get; } = new ProjectCollaboratorRole("READ_WRITE");
        
        /// <summary>Gets EDIT_ONLY.</summary>
        public static ProjectCollaboratorRole EditOnly { get; } = new ProjectCollaboratorRole("EDIT_ONLY");
        
        /// <summary>Gets COMPLETE_ONLY.</summary>
        public static ProjectCollaboratorRole CompleteOnly { get; } = new ProjectCollaboratorRole("COMPLETE_ONLY");
    }
}