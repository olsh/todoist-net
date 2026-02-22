using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents navigation settings.
    /// </summary>
    public class NavigationSettings
    {
        /// <summary>
        /// Gets a value indicating whether navigation counts are shown.
        /// </summary>
        [JsonPropertyName("counts_shown")]
        public bool CountsShown { get; internal set; }

        /// <summary>
        /// Gets the configured navigation features.
        /// </summary>
        [JsonPropertyName("features")]
        public IReadOnlyCollection<UserSettingsFeature> Features { get; internal set; }
    }
}
