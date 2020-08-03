using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    public abstract class Entity
    {
        private int? _hashCode;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        protected Entity()
        {
        }

        public override int GetHashCode()
        {
            if (!_hashCode.HasValue)
                _hashCode = HashCode.Combine(GetType(), Id);

            return _hashCode.Value;
        }

        public override bool Equals(object obj)
            => obj is Entity other
                && GetType() == other.GetType()
                && (Id.Equals(other.Id) || ReferenceEquals(this, other));



    }
}
