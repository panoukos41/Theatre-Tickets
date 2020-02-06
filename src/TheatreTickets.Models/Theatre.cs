using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TheatreTickets.Models
{
    public class Theatre
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public Location Location { get; set; }

        public string Directions { get; set; }

        public string Contact { get; set; }

        public string Info { get; set; }

        public ICollection<string> HallsIds { get; set; }

        [JsonIgnore]
        public ICollection<Hall> Halls { get; set; }

        public ICollection<string> PlaysIds { get; set; }

        [JsonIgnore]
        public ICollection<Play> Plays { get; set; }

        public ICollection<string> ImagesUrls { get; set; }

        public bool Published { get; set; }

        public Theatre()
        {
        }

        public Theatre(string id)
        {
            Id = id;
        }
    }
}