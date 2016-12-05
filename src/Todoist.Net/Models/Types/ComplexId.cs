using System;

namespace Todoist.Net.Models.Types
{
    public struct ComplexId : IEquatable<ComplexId>
    {
        public ComplexId(int persistentId)
            : this()
        {
            PersistentId = persistentId;
        }

        public ComplexId(Guid tempId)
            : this()
        {
            TempId = tempId;
        }

        public int PersistentId { get; }

        public Guid TempId { get; }

        public bool IsEmpty => PersistentId == default(int) && TempId == default(Guid);

        internal object DynamicId
        {
            get
            {
                if (PersistentId == default(int))
                {
                    return TempId;
                }

                return PersistentId;
            }
        }

        public static bool operator ==(ComplexId left, ComplexId right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ComplexId left, ComplexId right)
        {
            return !left.Equals(right);
        }

        public static implicit operator ComplexId(int i)
        {
            return new ComplexId(i);
        }

        public static implicit operator ComplexId(Guid g)
        {
            return new ComplexId(g);
        }

        public bool Equals(ComplexId other)
        {
            return (PersistentId == other.PersistentId) && TempId.Equals(other.TempId);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is ComplexId && Equals((ComplexId)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (PersistentId.GetHashCode() * 397) ^ TempId.GetHashCode();
            }
        }

        public override string ToString()
        {
            if (PersistentId != default(int))
            {
                return PersistentId.ToString();
            }

            if (TempId != default(Guid))
            {
                return TempId.ToString();
            }

            return string.Empty;
        }
    }
}
