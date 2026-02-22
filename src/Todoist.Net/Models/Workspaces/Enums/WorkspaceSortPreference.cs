namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a workspace sort preference.
    /// </summary>
    public class WorkspaceSortPreference : StringEnum
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WorkspaceSortPreference"/> class.
        /// </summary>
        /// <param name="value">The underlying API value.</param>
        private WorkspaceSortPreference(string value)
            : base(value)
        {
        }

        /// <summary>
        /// Gets the manual workspace sort preference.
        /// </summary>
        public static WorkspaceSortPreference Manual { get; } = new WorkspaceSortPreference("MANUAL");

        /// <summary>
        /// Gets the A to Z workspace sort preference.
        /// </summary>
        public static WorkspaceSortPreference AToZ { get; } = new WorkspaceSortPreference("A_TO_Z");

        /// <summary>
        /// Gets the Z to A workspace sort preference.
        /// </summary>
        public static WorkspaceSortPreference ZToA { get; } = new WorkspaceSortPreference("Z_TO_A");
    }
}