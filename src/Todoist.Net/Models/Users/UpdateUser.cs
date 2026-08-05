using System.Collections.Generic;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a Todoist user.
    /// </summary>
    public class UpdateUser : BaseUser, ICommandArgument, IUnsettableProperties
    {
        HashSet<PropertyInfo> IUnsettableProperties.UnsetProperties { get; } = new HashSet<PropertyInfo>();


        /// <summary>
        /// The user's current password.
        /// </summary>
        /// <remarks>
        /// This must be provided if the request is modifying the user's password or email address and the user already has a password set (indicated by has_password in the user object).
        /// For amending other properties this is not required.
        /// </remarks>
        [JsonPropertyName("current_password")]
        public string CurrentPassword { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        [JsonPropertyName("password")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the time zone.
        /// </summary>
        [JsonPropertyName("timezone")]
        public string TimeZone { get; set; }

        /// <summary>
        /// Gets or sets the currently selected Todoist theme.
        /// </summary>
        [JsonPropertyName("theme")]
        public int? Theme { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user is included in the beta testing group.
        /// </summary>
        [JsonPropertyName("beta")]
        public bool? Beta { get; set; }
    }
}
