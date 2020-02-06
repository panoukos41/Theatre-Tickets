using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TheatreTickets.Models
{
    public class Hall
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string TheatreId { get; set; }

        [JsonIgnore]
        public Theatre Theatre { get; set; }

        public ICollection<Seat> Seats { get; set; }

        public ICollection<string> ImagesUrls { get; set; }

        public bool Published { get; set; }
    }
}