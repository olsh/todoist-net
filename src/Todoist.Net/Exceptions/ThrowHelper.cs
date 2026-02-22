using System;
using System.Collections.Generic;

using Todoist.Net.Models;

namespace Todoist.Net.Exceptions
{
    internal static class ThrowHelper
    {
        public static void ThrowIfNull<T>(T value, string paramName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }

        public static void ThrowIfDefaultOrEmpty(ComplexId value, string paramName)
        {
            if (value == default || value.IsEmpty)
            {
                throw new ArgumentException("Entity ID cannot be empty.", paramName);
            }
        }

        public static void ThrowIfNullOrEmpty(string value, string paramName)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Value cannot be null or empty.", paramName);
            }
        }

        public static void ThrowIfNullOrWhiteSpace(string value, string paramName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", paramName);
            }
        }

        public static void ThrowIfNullOrEmpty<T>(ICollection<T> collection, string paramName)
        {
            if (collection == null || collection.Count == 0)
            {
                throw new ArgumentException("Value cannot be null or an empty collection.", paramName);
            }
        }
    }
}
