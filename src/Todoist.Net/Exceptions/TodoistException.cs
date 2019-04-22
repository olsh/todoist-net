using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Todoist.Net.Exceptions
{
    /// <summary>
    ///     Represents an errors that occur during requests to Todoist API.
    /// </summary>
    /// <seealso cref="System.Exception" />
    [Serializable]
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
        public TodoistException(long code, string message, dynamic rawError)
            : base(message)
        {
            Code = code;
            RawError = rawError;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TodoistException" /> class.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner exception.</param>
        public TodoistException(long code, string message, Exception inner)
            : base(message, inner)
        {
            Code = code;
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        private TodoistException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Code = info.GetInt64(nameof(Code));
        }

        /// <summary>
        ///     Gets the code.
        /// </summary>
        /// <value>The code.</value>
        public long Code { get; }

        /// <summary>
        ///     Gets the raw error.
        /// </summary>
        /// <value>The raw error.</value>
        public dynamic RawError { get; }

        /// <inheritdoc />
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                // ReSharper disable once ExceptionNotDocumented
                throw new ArgumentNullException(nameof(info));
            }

            // ReSharper disable once ExceptionNotDocumented
            info.AddValue(nameof(Code), Code);

            base.GetObjectData(info, context);
        }
    }
}
