using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents the plan limits for a user.
    /// </summary>
    public class UserPlanLimits
    {
        /// <summary>
        /// Gets the current plan limits.
        /// </summary>
        [JsonPropertyName("current")]
        public PlanLimits Current { get; internal set; }

        /// <summary>
        /// Gets the next plan limits.
        /// </summary>
        [JsonPropertyName("next")]
        public PlanLimits Next { get; internal set; }
    }
}
