namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents workspace acquisition source values.
    /// </summary>
    public class WorkspaceAcquisitionSource : StringEnum
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WorkspaceAcquisitionSource"/> class.
        /// </summary>
        /// <param name="value">The underlying API value.</param>
        private WorkspaceAcquisitionSource(string value) : base(value) { }

        /// <summary>Gets high_paid_channel.</summary>
        public static WorkspaceAcquisitionSource HighPaidChannel { get; } = new WorkspaceAcquisitionSource("high_paid_channel");
    }
}