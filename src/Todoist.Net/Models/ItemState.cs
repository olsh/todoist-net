namespace Todoist.Net.Models
{
    /// <summary>
    ///     Represents a state of a Todoist task.
    /// </summary>
    public class ItemState : OrderIndentEntry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ItemState" /> class.
        /// </summary>
        /// <param name="itemId">The item identifier.</param>
        /// <param name="isChecked">The is checked.</param>
        /// <param name="indent">The indent.</param>
        /// <param name="inHistory">The in history.</param>
        /// <param name="order">The order.</param>
        public ItemState(ComplexId itemId, bool isChecked, int indent, bool inHistory, int order)
            : base(itemId, order, indent)
        {
            IsChecked = isChecked;
            InHistory = inHistory;
        }

        /// <summary>
        ///     Gets a value indicating whether [in history].
        /// </summary>
        /// <value><c>true</c> if [in history]; otherwise, <c>false</c>.</value>
        public bool InHistory { get; }

        /// <summary>
        ///     Gets a value indicating whether this instance is checked.
        /// </summary>
        /// <value><c>true</c> if this instance is checked; otherwise, <c>false</c>.</value>
        public bool IsChecked { get; }

        internal override int[] ToArray()
        {
            var array = new int[4];

            array[0] = InHistory ? 1 : 0;

            array[1] = IsChecked ? 1 : 0;

            array[2] = Order;

            array[3] = Indent;

            return array;
        }
    }
}
