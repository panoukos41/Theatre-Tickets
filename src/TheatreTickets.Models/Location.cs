namespace TheatreTickets.Models
{
    public class Location
    {
        public string Name { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public Location()
        {
        }

        public Location(double lat, double lon)
        {
            Latitude = lat;
            Longitude = lon;
        }

        public Location(string name, double lat, double lon)
            : this(lat, lon)
        {
            Name = name;
        }
    }
}