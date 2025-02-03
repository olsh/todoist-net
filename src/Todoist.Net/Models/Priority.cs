namespace Todoist.Net.Models
{
    /// <summary>
    /// Priority of a task.
    /// </summary>
    /// <remarks>
    /// The priority of the task (a number between 1 and 4, 4 for very urgent and 1 for natural).
    /// Note: Keep in mind that very urgent is the priority 1 on clients. So, p1 will return 4 in the API.
    /// </remarks>
    public enum Priority : byte
    {
        /// <summary>
        /// The priority1
        /// </summary>
        Priority1 = 4,

        /// <summary>
        /// The priority2
        /// </summary>
        Priority2 = 3,

        /// <summary>
        /// The priority3
        /// </summary>
        Priority3 = 2,

        /// <summary>
        /// The priority4
        /// </summary>
        Priority4 = 1
    }
}
