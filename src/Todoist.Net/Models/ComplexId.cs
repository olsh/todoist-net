using System;

namespace Todoist.Net.Models
{
    /// <summary>
    /// Struct that represents an identifier of Todoist entities. Can be <see cref="System.Int32"/> if it's persistent or <see cref="Guid"/> if it's temporary.
    /// </summary>
    public struct ComplexId : IEquatable<ComplexId>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ComplexId"/> struct.
        /// </summary>
        /// <param name="persistentId">The persistent identifier.</param>
        public ComplexId(string persistentId)
            : this()
        {
            PersistentId = persistentId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ComplexId"/> struct.
        /// </summary>
        /// <param name="tempId">The temporary identifier.</param>
        public ComplexId(Guid tempId)
            : this()
        {
            TempId = tempId;
        }

        /// <summary>
        /// Gets the persistent identifier.
        /// </summary>
        /// <value>The persistent identifier.</value>
        public string PersistentId { get; }

        /// <summary>
        /// Gets the temporary identifier.
        /// </summary>
        /// <value>The temporary identifier.</value>
        public Guid TempId { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is empty.
        /// </summary>
        /// <value><c>true</c> if this instance is empty; otherwise, <c>false</c>.</value>
        public bool IsEmpty => PersistentId == default && TempId == default;

        internal object DynamicId
        {
            get
            {
                if (PersistentId == default)
                {
                    return TempId;
                }

                return PersistentId;
            }
        }

        /// <summary>
        /// Implements the == operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(ComplexId left, ComplexId right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Implements the != operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(ComplexId left, ComplexId right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.Int64"/> to <see cref="ComplexId"/>.
        /// </summary>
        /// <param name="i">The i.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator ComplexId(string i)
        {
            return new ComplexId(i);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="Guid"/> to <see cref="ComplexId"/>.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator ComplexId(Guid g)
        {
            return new ComplexId(g);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
        public bool Equals(ComplexId other)
        {
            return (PersistentId == other.PersistentId) && TempId.Equals(other.TempId);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is ComplexId a && Equals(a);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return (PersistentId.GetHashCode() * 397) ^ TempId.GetHashCode();
            }
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            if (!string.IsNullOrEmpty(PersistentId))
            {
                return PersistentId;
            }

            if (TempId != default)
            {
                return TempId.ToString();
            }

            return string.Empty;
        }
    }
}
