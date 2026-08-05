namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a workspace payload for add requests.
    /// </summary>
    public class AddWorkspace : BaseWorkspace
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddWorkspace" /> class.
        /// </summary>
        /// <param name="name">The workspace name.</param>
        public AddWorkspace(string name)
        {
            Name = name;
        }

    }
}
