namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents an order entry which can be used to specify order and indentation of a Todoist entity.
    /// </summary>
    /// <seealso cref="Todoist.Net.Models.OrderEntry" />
    public class OrderIndentEntry : OrderEntry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderIndentEntry"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="order">The order.</param>
        /// <param name="indent">The indent.</param>
        public OrderIndentEntry(ComplexId id, int order, int indent)
            : base(id, order)
        {
            Indent = indent;
        }

        /// <summary>
        /// Gets the indent.
        /// </summary>
        /// <value>The indent.</value>
        public int Indent { get; }

        internal virtual int[] ToArray()
        {
            var array = new int[2];

            array[0] = Order;

            array[1] = Indent;

            return array;
        }
    }
}
