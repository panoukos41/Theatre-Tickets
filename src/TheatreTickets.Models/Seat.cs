using System;

namespace TheatreTickets.Models
{
    public class Seat : IEquatable<Seat>
    {
        public int Number { get; set; }

        public int Row { get; set; }

        public override string ToString()
            => $"{Row}:{Number}";

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            else if (ReferenceEquals(this, obj))
                return true;
            else if (obj is Seat seat)
                return Equals(seat);
            else
                return false;
        }

        public bool Equals(Seat other) 
            => other.Number.Equals(Number)
               && other.Row.Equals(Row);

        public override int GetHashCode() 
            => Number.GetHashCode() ^ Row.GetHashCode();
    }
}