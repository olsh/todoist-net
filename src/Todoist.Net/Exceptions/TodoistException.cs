using System;
using System.Collections.Generic;
#if NETFRAMEWORK
using System.Runtime.Serialization;
#endif

using Todoist.Net.Models;

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
        ///     Initializes a new instance of the <see cref="TodoistException" /> class.
        /// </summary>
        public TodoistException()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TodoistException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public TodoistException(string message)
            : base(message)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TodoistException" /> class.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="message">The message.</param>
        public TodoistException(int code, string message)
            : base(message)
        {
            Code = code;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TodoistException" /> class.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="message">The message.</param>
        /// <param name="rawError">The raw error.</param>
        public TodoistException(int code, string message, CommandError rawError)
            : base(message)
        {
            Code = code;
            RawError = rawError;

            if (rawError != null)
            {
                ErrorTag = rawError.ErrorTag;
                HttpCode = rawError.HttpCode;
                ErrorExtra = rawError.ErrorExtra;

                ValidateApiErrorDetails(ErrorTag, HttpCode, ErrorExtra);
            }
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TodoistException" /> class.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner exception.</param>
        public TodoistException(int code, string message, Exception inner)
            : base(message, inner)
        {
            Code = code;
        }

#if NETFRAMEWORK
        /// <summary>
        ///     Initializes a new instance of the <see cref="TodoistException" /> class during deserialization.
        /// </summary>
        /// <param name="info">The serialization info.</param>
        /// <param name="context">The streaming context.</param>
        private TodoistException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Code = info.GetInt32(nameof(Code));
            RawError = (CommandError)info.GetValue(nameof(RawError), typeof(CommandError));
            ErrorTag = info.GetString(nameof(ErrorTag));
            HttpCode = info.GetInt32(nameof(HttpCode));
            ErrorExtra = (Dictionary<string, object>)info.GetValue(nameof(ErrorExtra), typeof(Dictionary<string, object>));

            ValidateApiErrorDetails(ErrorTag, HttpCode, ErrorExtra);
        }
#endif

        private static void ValidateApiErrorDetails(string errorTag, int httpCode, Dictionary<string, object> errorExtra)
        {
            if (errorTag != null && errorTag.Length == 0)
            {
                throw new ArgumentException("Error tag cannot be empty.", nameof(errorTag));
            }

            if (httpCode < 0 || httpCode > 999)
            {
                throw new ArgumentOutOfRangeException(nameof(httpCode), httpCode, "HTTP code must be between 0 and 999.");
            }

            if (errorExtra != null)
            {
                foreach (var key in errorExtra.Keys)
                {
                    if (key == null)
                    {
                        throw new ArgumentException("Error extra contains a null key.", nameof(errorExtra));
                    }
                }
            }
        }

        /// <summary>
        ///     Gets the error code.
        /// </summary>
        /// <value>The error code.</value>
        public int Code { get; }

        /// <summary>
        ///     Gets the raw error.
        /// </summary>
        /// <value>The raw error.</value>
        public CommandError RawError { get; }

        /// <summary>
        ///     Gets the error tag.
        /// </summary>
        /// <value>The error tag (e.g., "NOT_FOUND", "INVALID_ARGUMENT_VALUE").</value>
        public string ErrorTag { get; }

        /// <summary>
        ///     Gets the HTTP status code.
        /// </summary>
        /// <value>The HTTP status code.</value>
        public int HttpCode { get; }

        /// <summary>
        ///     Gets the extra error information.
        /// </summary>
        /// <value>A dictionary containing additional error details.</value>
        public Dictionary<string, object> ErrorExtra { get; }

#if NETFRAMEWORK
        /// <inheritdoc />
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            info.AddValue(nameof(Code), Code);
            info.AddValue(nameof(RawError), RawError);
            info.AddValue(nameof(ErrorTag), ErrorTag);
            info.AddValue(nameof(HttpCode), HttpCode);
            info.AddValue(nameof(ErrorExtra), ErrorExtra);

            base.GetObjectData(info, context);
        }
#endif
    }
}
