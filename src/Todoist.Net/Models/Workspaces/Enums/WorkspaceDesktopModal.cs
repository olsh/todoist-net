namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents desktop workspace modal display modes.
    /// </summary>
    public class WorkspaceDesktopModal : StringEnum
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WorkspaceDesktopModal"/> class.
        /// </summary>
        /// <param name="value">The underlying API value.</param>
        private WorkspaceDesktopModal(string value)
            : base(value)
        {
        }

        /// <summary>
        /// Gets the trial-offer modal mode.
        /// </summary>
        public static WorkspaceDesktopModal TrialOffer { get; } = new WorkspaceDesktopModal("TRIAL_OFFER");
    }
}