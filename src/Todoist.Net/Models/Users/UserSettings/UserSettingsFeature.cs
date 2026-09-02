using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a navigation feature setting.
    /// </summary>
    public class UserSettingsFeature
    {
        /// <summary>
        /// Gets the feature name.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether the feature is shown.
        /// </summary>
        [JsonPropertyName("shown")]
        public bool Shown { get; internal set; }
    }
}
