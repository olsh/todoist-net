using System;

namespace Todoist.Net.Exceptions
{
    public class TodoistException : Exception
    {
        public TodoistException()
        {
        }

        public TodoistException(string message)
            : base(message)
        {
        }

        public TodoistException(int code, string message)
            : base(message)
        {
            Code = code;
        }

        public TodoistException(int code, string message, dynamic rawError)
            : base(message)
        {
            Code = code;
            RawError = rawError;
        }

        public TodoistException(int code, string message, Exception inner)
            : base(message, inner)
        {
            Code = code;
        }

        public int Code { get; }

        public dynamic RawError { get; }
    }
}
