namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents time formats.
    /// </summary>
    public enum TimeFormat : byte
    {
        /// <summary>
        /// The hour24
        /// </summary>
        /// <remarks>24h format such as 13:00.</remarks>
        Hour24 = 0,

        /// <summary>
        /// The hour12
        /// </summary>
        /// <remarks>12h format such as 1:00pm.</remarks>
        Hour12 = 1
    }
}
