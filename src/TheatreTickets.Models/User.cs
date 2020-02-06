using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TheatreTickets.Models
{
    public class User
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public UserType UserType { get; set; }

        public ICollection<string> PlaysWatchedIds { get; set; }

        [JsonIgnore]
        public ICollection<Play> PlaysWatched { get; set; }

        public ICollection<string> TicketsIds { get; set; }

        [JsonIgnore]
        public ICollection<Ticket> Tickets { get; set; }

        public User()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}