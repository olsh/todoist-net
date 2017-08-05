using System;
using System.Collections.Generic;
using System.Reflection;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Represents a base class of string enum type.
    /// </summary>
    public abstract class StringEnum : IEquatable<StringEnum>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StringEnum" /> class.
        /// </summary>
        /// <param name="value">The value.</param>
        protected StringEnum(string value)
        {
            Value = value;
        }

        internal string Value { get; }

        /// <summary>
        /// Implements the == operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(StringEnum left, StringEnum right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Implements the != operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(StringEnum left, StringEnum right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        /// Tries the parse.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="type">The type.</param>
        /// <param name="result">The result.</param>
        /// <returns><c>true</c> if the parsing was successful, <c>false</c> otherwise.</returns>
        public static bool TryParse(string value, Type type, out StringEnum result)
        {
            result = null;

            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            if (type == null || !type.GetTypeInfo().IsSubclassOf(typeof(StringEnum)))
            {
                return false;
            }

            IEnumerable<PropertyInfo> properties = type.GetTypeInfo().DeclaredProperties;
            foreach (PropertyInfo property in properties)
            {
                var stringEnum = property.GetValue(null) as StringEnum;
                if (stringEnum != null && stringEnum.Value == value)
                {
                    result = stringEnum;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
        public virtual bool Equals(StringEnum other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return string.Equals(Value, other.Value);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((StringEnum)obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return Value;
        }
    }
}
