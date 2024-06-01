namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a Todoist task.
    /// </summary>
    public class UpdateItem : BaseItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateItem" /> class.
        /// </summary>
        /// <param name="id">The id of the item to update.</param>
        public UpdateItem(ComplexId id)
            : base(id)
        {
        }
    }
}
