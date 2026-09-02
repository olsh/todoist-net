using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents quick add settings.
    /// </summary>
    public class QuickAddSettings
    {
        /// <summary>
        /// Gets a value indicating whether labels are shown in quick add.
        /// </summary>
        [JsonPropertyName("labels_shown")]
        public bool LabelsShown { get; internal set; }

        /// <summary>
        /// Gets the configured quick add features.
        /// </summary>
        [JsonPropertyName("features")]
        public IReadOnlyCollection<UserSettingsFeature> Features { get; internal set; }
    }
}
