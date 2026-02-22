namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents project status values.
    /// </summary>
    public class ProjectStatus : StringEnum
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectStatus"/> class.
        /// </summary>
        /// <param name="value">The underlying API value.</param>
        private ProjectStatus(string value) : base(value) { }

        /// <summary>Gets PLANNED.</summary>
        public static ProjectStatus Planned { get; } = new ProjectStatus("PLANNED");
        
        /// <summary>Gets IN_PROGRESS.</summary>
        public static ProjectStatus InProgress { get; } = new ProjectStatus("IN_PROGRESS");
        
        /// <summary>Gets PAUSED.</summary>
        public static ProjectStatus Paused { get; } = new ProjectStatus("PAUSED");
        
        /// <summary>Gets COMPLETED.</summary>
        public static ProjectStatus Completed { get; } = new ProjectStatus("COMPLETED");
        
        /// <summary>Gets CANCELED.</summary>
        public static ProjectStatus Canceled { get; } = new ProjectStatus("CANCELED");
    }
}