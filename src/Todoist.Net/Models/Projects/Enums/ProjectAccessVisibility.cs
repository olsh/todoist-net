namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents project access visibility values.
    /// </summary>
    public class ProjectAccessVisibility : StringEnum
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectAccessVisibility"/> class.
        /// </summary>
        /// <param name="value">The underlying API value.</param>
        private ProjectAccessVisibility(string value) : base(value) { }

        /// <summary>Gets restricted visibility.</summary>
        public static ProjectAccessVisibility Restricted { get; } = new ProjectAccessVisibility("restricted");
        
        /// <summary>Gets team visibility.</summary>
        public static ProjectAccessVisibility Team { get; } = new ProjectAccessVisibility("team");
        
        /// <summary>Gets public visibility.</summary>
        public static ProjectAccessVisibility Public { get; } = new ProjectAccessVisibility("public");
    }
}