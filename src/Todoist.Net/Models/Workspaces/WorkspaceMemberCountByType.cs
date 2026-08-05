using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents member counts by role type.
    /// </summary>
    public class WorkspaceMemberCountByType
    {
        /// <summary>
        /// Gets the admin count.
        /// </summary>
        [JsonPropertyName("admin_count")]
        public int? AdminCount { get; internal set; }

        /// <summary>
        /// Gets the member count.
        /// </summary>
        [JsonPropertyName("member_count")]
        public int? MemberCount { get; internal set; }

        /// <summary>
        /// Gets the guest count.
        /// </summary>
        [JsonPropertyName("guest_count")]
        public int? GuestCount { get; internal set; }

    }
}
