using System;
using System.Text;
using System.Text.Json;

using Todoist.Net.Models;

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
        private static readonly JsonSerializerOptions _defaultSerializationOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web) { WriteIndented = true };


        /// <summary>
        /// Initializes a new instance of the <see cref="TodoistException" /> class using a <see cref="TodoistError" /> object.
        /// </summary>
        /// <param name="error">The <see cref="TodoistError" /> object containing error details.</param>
        /// <param name="inner">The inner exception.</param>
        public TodoistException(TodoistError error, Exception inner = null)
            : this(error?.Error, error?.ErrorCode, error?.ErrorTag, error?.HttpCode, error?.ErrorExtra, inner)
        {
        }

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
            TodoistErrorExtra errorExtra = null, 
            Exception inner = null)
            : base(GetFullMessage(message, code, errorTag, httpCode, errorExtra), inner)
        {
            Code = code;
            ErrorTag = errorTag;
            HttpCode = httpCode;
            ErrorExtra = errorExtra;
        }

        private static string GetFullMessage(string message, int? code, string errorTag, int? httpCode, TodoistErrorExtra errorExtra)
        {
            var stringBuilder = new StringBuilder(message ?? "An error occurred while processing the request to the Todoist API.");
            if (code.HasValue)
            {
                stringBuilder.Append($" - Error Code: {code.Value}");
            }
            if (!string.IsNullOrEmpty(errorTag))
            {
                stringBuilder.Append($" - Error Tag: {errorTag}");
            }
            if (httpCode.HasValue)
            {
                stringBuilder.Append($" - HTTP Status Code: {httpCode.Value}");
            }

            if (errorExtra != null)
            {
                stringBuilder.AppendLine();
                stringBuilder.AppendLine("Error Extra:");
                stringBuilder.Append(JsonSerializer.Serialize(errorExtra, _defaultSerializationOptions));
            }
            return stringBuilder.ToString();
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
            ErrorExtra = (TodoistErrorExtra)info.GetValue(nameof(ErrorExtra), typeof(TodoistErrorExtra));
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
        /// <value>Additional error details (e.g., "event_id", "retry_after").</value>
        public TodoistErrorExtra ErrorExtra { get; }

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
