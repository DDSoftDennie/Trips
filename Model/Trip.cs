using Model.Enums;
using System.Collections;

namespace Model
{
   
    public class Trips
    {
        private List<Trip> _trips;
        public Trips()
        {
            _trips = new List<Trip>();
        }
        public void AddTrip(Trip trip)
        {
            if (trip != null)
                _trips.Add(trip);
            else
                throw new Exception();
        }
        
        public void AddTrips(List<Trip> trips)
        {
            if (trips != null)
                _trips.AddRange(trips);
            else
                throw new Exception();
        }
        
        public IEnumerable<Trip> GetTrips()
        {
            return _trips;
        }
        
    }
    public class Trip
    {
        public Trip()
        { }
        
        public Trip(DateOnly date, string tripName, string city, string country, TripType type)
        {
            Date = date;
            TripName = tripName;
            City = city;
            Country = country;
            Type = type;
        }
        public DateOnly Date { get; set; }
        public string TripName { get; set; } = "";

        public string City { get; set; } = "";
        public string Country { get; set; } = "";

        public TripType Type { get; set; }
    }

}