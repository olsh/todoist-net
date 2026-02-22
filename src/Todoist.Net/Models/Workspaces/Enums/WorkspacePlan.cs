namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a workspace subscription plan.
    /// </summary>
    public class WorkspacePlan : StringEnum
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WorkspacePlan"/> class.
        /// </summary>
        /// <param name="value">The underlying API value.</param>
        private WorkspacePlan(string value)
            : base(value)
        {
        }

        /// <summary>
        /// Gets the starter workspace plan.
        /// </summary>
        public static WorkspacePlan Starter { get; } = new WorkspacePlan("STARTER");

        /// <summary>
        /// Gets the business workspace plan.
        /// </summary>
        public static WorkspacePlan Business { get; } = new WorkspacePlan("BUSINESS");
    }
}