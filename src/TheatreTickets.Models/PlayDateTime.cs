using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TheatreTickets.Models
{
    public class PlayDateTime
    {
        public string Id { get; set; }

        public string PlayId { get; set; }

        [JsonIgnore]
        public Play Play { get; set; }

        public string HallId { get; set; }

        [JsonIgnore]
        public Hall Hall { get; set; }

        public DateTime DateTime { get; set; }

        public ICollection<Seat> SeatsTaken { get; set; }

        public bool Published { get; set; }
    }
}