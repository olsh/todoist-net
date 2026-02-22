using System;
using System.Collections.Generic;

#if NETFRAMEWORK
using System.Runtime.Serialization;
#endif

namespace Todoist.Net.Exceptions
{
    /// <summary>
    ///     Represents an errors that occur during requests to Todoist API.
    /// </summary>
    /// <seealso cref="System.Exception" />
#if NETFRAMEWORK
    [Serializable]
#endif
    public sealed class TodoistException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TodoistException" /> class.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="message">The message.</param>
        /// <param name="errorTag">The error tag (e.g., "NOT_FOUND", "INVALID_ARGUMENT_VALUE").</param>
        /// <param name="httpCode">The HTTP status code.</param>
        /// <param name="errorExtra">A dictionary containing additional error details (e.g., "event_id", "retry_after").</param>
        /// <param name="inner">The inner exception.</param>
        public TodoistException(
            string message = null,
            int? code = null,
            string errorTag = null, 
            int? httpCode = null, 
            IDictionary<string, object> errorExtra = null, 
            Exception inner = null)
            : base(message, inner)
        {
            Code = code;
            ErrorTag = errorTag;
            HttpCode = httpCode;
            ErrorExtra = errorExtra;
        }

#if NETFRAMEWORK
        /// <summary>
        /// Initializes a new instance of the <see cref="TodoistException" /> class during deserialization.
        /// </summary>
        /// <param name="info">The serialization info.</param>
        /// <param name="context">The streaming context.</param>
        private TodoistException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Code = (int?)info.GetValue(nameof(Code), typeof(int?));
            ErrorTag = info.GetString(nameof(ErrorTag));
            HttpCode = (int?)info.GetValue(nameof(HttpCode), typeof(int?));
            ErrorExtra = (IDictionary<string, object>)info.GetValue(nameof(ErrorExtra), typeof(IDictionary<string, object>));
        }
#endif

        /// <summary>
        /// Gets the error code.
        /// </summary>
        /// <value>The error code.</value>
        public int? Code { get; }

        /// <summary>
        /// Gets the error tag.
        /// </summary>
        /// <value>The error tag (e.g., "NOT_FOUND", "INVALID_ARGUMENT_VALUE").</value>
        public string ErrorTag { get; }

        /// <summary>
        /// Gets the HTTP status code.
        /// </summary>
        /// <value>The HTTP status code.</value>
        public int? HttpCode { get; }

        /// <summary>
        /// Gets the extra error information.
        /// </summary>
        /// <value>A dictionary containing additional error details.</value>
        public IDictionary<string, object> ErrorExtra { get; }

#if NETFRAMEWORK
        /// <inheritdoc />
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            info.AddValue(nameof(Code), Code);
            info.AddValue(nameof(ErrorTag), ErrorTag);
            info.AddValue(nameof(HttpCode), HttpCode);
            info.AddValue(nameof(ErrorExtra), ErrorExtra);

            base.GetObjectData(info, context);
        }
#endif
    }
}
