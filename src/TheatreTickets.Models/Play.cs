using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TheatreTickets.Models
{
    public class Play
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public float Duration { get; set; }

        public ICollection<string> ImagesUrls { get; set; }

        public string TheatreId { get; set; }

        [JsonIgnore]
        public Theatre Theatre { get; set; }

        public ICollection<string> PlayDateTimesIds { get; set; }

        [JsonIgnore]
        public ICollection<PlayDateTime> PlayDateTimes { get; set; }

        public bool Published { get; set; }
    }
}