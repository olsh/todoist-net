namespace Todoist.Net.Models
{
    /// <summary>
    /// Class ObjectEventTypes.
    /// </summary>
    public class ObjectEventTypes
    {
        /// <summary>
        /// Gets or sets the type of the event.
        /// </summary>
        /// <value>The type of the event.</value>
        public string EventType { get; set; }

        /// <summary>
        /// Gets or sets the type of the object.
        /// </summary>
        /// <value>The type of the object.</value>
        public string ObjectType { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return $"\"{ObjectType}:{EventType}\"";
        }
    }
}
