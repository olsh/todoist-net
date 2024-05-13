using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represent a collection of Todoist resources.
    /// </summary>
    public class Resources
    {
        /// <summary>
        /// Gets the filters.
        /// </summary>
        /// <value>The filters.</value>
        [JsonPropertyName("filters")]
        public IReadOnlyCollection<Filter> Filters { get; internal set; }

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <value>The items.</value>
        [JsonPropertyName("items")]
        public IReadOnlyCollection<Item> Items { get; internal set; }

        /// <summary>
        /// Gets the labels.
        /// </summary>
        /// <value>The labels.</value>
        [JsonPropertyName("labels")]
        public IReadOnlyCollection<Label> Labels { get; internal set; }

        /// <summary>
        /// Gets or sets the last read notification identifier.
        /// </summary>
        /// <value>The last read notification identifier.</value>
        [JsonPropertyName("live_notifications_last_read_id")]
        public long? LastReadNotificationId { get; set; }

        /// <summary>
        /// Gets the notes.
        /// </summary>
        /// <value>The notes.</value>
        [JsonPropertyName("notes")]
        public IReadOnlyCollection<Note> Notes { get; internal set; }

        /// <summary>
        /// Gets the notifications.
        /// </summary>
        /// <value>The notifications.</value>
        [JsonPropertyName("live_notifications")]
        public IReadOnlyCollection<Notification> Notifications { get; internal set; }

        /// <summary>
        /// Gets the project notes.
        /// </summary>
        /// <value>The project notes.</value>
        [JsonPropertyName("project_notes")]
        public IReadOnlyCollection<Note> ProjectNotes { get; internal set; }

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <value>The projects.</value>
        [JsonPropertyName("projects")]
        public IReadOnlyCollection<Project> Projects { get; internal set; }

        /// <summary>
        /// Gets the reminders.
        /// </summary>
        /// <value>The reminders.</value>
        [JsonPropertyName("reminders")]
        public IReadOnlyCollection<Reminder> Reminders { get; internal set; }

        /// <summary>
        /// Gets the sections.
        /// </summary>
        /// <value>
        /// The sections.
        /// </value>
        [JsonPropertyName("sections")]
        public IReadOnlyCollection<Section> Sections { get; internal set; }

        /// <summary>
        /// Gets the collaborators.
        /// </summary>
        /// <value>
        /// The collaborators.
        /// </value>
        [JsonPropertyName("collaborators")]
        public IReadOnlyCollection<Collaborator> Collaborators { get; internal set; }

        /// <summary>
        /// Gets the collaborator states.
        /// </summary>
        /// <value>
        /// The collaborator states.
        /// </value>
        [JsonPropertyName("collaborator_states")]
        public IReadOnlyCollection<CollaboratorState> CollaboratorStates { get; internal set; }

        /// <summary>
        /// Gets the user information.
        /// </summary>
        /// <value>The user information.</value>
        [JsonPropertyName("user")]
        public UserInfo UserInfo { get; internal set; }

        /// <summary>
        /// The Sync_Token returned from Todoist for incremental Sync
        /// </summary>
        /// <value>The Sync Token</value>
        [JsonPropertyName("sync_token")]
        public string SyncToken { get; internal set; }
    }
}
