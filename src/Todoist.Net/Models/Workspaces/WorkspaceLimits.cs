using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents workspace limits grouped by current and next plans.
    /// </summary>
    public class WorkspaceLimits
    {
        /// <summary>
        /// Gets current-plan limits.
        /// </summary>
        [JsonPropertyName("current")]
        public WorkspaceLimitSet Current { get; internal set; }

        /// <summary>
        /// Gets next-plan limits.
        /// </summary>
        [JsonPropertyName("next")]
        public WorkspaceLimitSet Next { get; internal set; }
    }
}