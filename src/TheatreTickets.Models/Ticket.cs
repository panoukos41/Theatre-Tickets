using System.Text.Json.Serialization;

namespace TheatreTickets.Models
{
    public class Ticket
    {
        public string Id { get; set; }

        public Seat Seat { get; set; }

        public string ForDateId { get; set; }

        [JsonIgnore]
        public PlayDateTime ForDate { get; set; }

        public Ticket()
        {
        }

        public Ticket(string id)
        {
            Id = id;
        }
    }
}