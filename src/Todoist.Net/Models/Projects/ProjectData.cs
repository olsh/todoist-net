using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a Todoist project and its uncompleted tasks.
    /// </summary>
    public class ProjectData
    {
        /// <summary>Gets the tasks.</summary>
        /// <remarks>The JSON property name remains "items" for backwards compatibility with Sync API.</remarks>
        [JsonPropertyName("items")]
        public IReadOnlyCollection<TaskInfo> Tasks { get; internal set; }

        /// <summary>Gets the comments.</summary>
        /// <remarks>The JSON property name remains "project_notes" for backwards compatibility with Sync API.</remarks>
        [JsonPropertyName("project_notes")]
        public IReadOnlyCollection<Comment> ProjectComments { get; internal set; }

        /// <summary>Gets the collaborators.</summary>
        [JsonPropertyName("collaborators")]
        public IReadOnlyCollection<Collaborator> Collaborators { get; internal set; }

        /// <summary>Gets the collaborator states.</summary>
        [JsonPropertyName("collaborator_states")]
        public IReadOnlyCollection<CollaboratorState> CollaboratorStates { get; internal set; }

        /// <summary>Gets the sections.</summary>
        [JsonPropertyName("sections")]
        public IReadOnlyCollection<Section> Sections { get; internal set; }

        /// <summary>Gets the sub-projects.</summary>
        [JsonPropertyName("subprojects")]
        public IReadOnlyCollection<ProjectInfo> SubProjects { get; internal set; }

        /// <summary>
        /// Gets the project.
        /// </summary>
        /// <value>The project.</value>
        [JsonPropertyName("project")]
        public ProjectInfo Project { get; internal set; }

        /// <summary>
        /// Gets the workspace folder.
        /// </summary>
        /// <value>The workspace folder.</value>
        [JsonPropertyName("folder")]
        public WorkspaceFolder Folder { get; internal set; }
    }
}
