namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents an order entry which can be used to specify order of a Todoist entity.
    /// </summary>
    public class OrderEntry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderEntry"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="order">The order.</param>
        public OrderEntry(ComplexId id, int order)
        {
            Id = id;
            Order = order;
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public ComplexId Id { get; }

        /// <summary>
        /// Gets the order.
        /// </summary>
        /// <value>The order.</value>
        public int Order { get; }
    }
}
