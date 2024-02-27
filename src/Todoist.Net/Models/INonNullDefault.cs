namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a type that has a default value other than null.
    /// </summary>
    /// <remarks>
    /// Non-null default values are typically used to be ignored during serialization
    /// when null values should be included. 
    /// </remarks>
    internal interface INonNullDefault
    {
        /// <summary>
        /// Gets a value indicating whether the current instance represents the type's default value.
        /// </summary>
        bool IsDefault { get; }
    }
}
