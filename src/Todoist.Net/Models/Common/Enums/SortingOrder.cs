namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents sorting order values.
    /// </summary>
    public class SortingOrder : StringEnum
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SortingOrder"/> class.
        /// </summary>
        /// <param name="value">The underlying API value.</param>
        private SortingOrder(string value) : base(value) { }

        /// <summary>Gets ascending sorting order.</summary>
        public static SortingOrder Asc { get; } = new SortingOrder("asc");

        /// <summary>Gets descending sorting order.</summary>
        public static SortingOrder Desc { get; } = new SortingOrder("desc");
    }
}