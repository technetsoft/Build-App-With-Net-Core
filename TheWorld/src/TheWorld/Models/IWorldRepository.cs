using System.Collections.Generic;
using System.Threading.Tasks;

namespace TheWorld.Models
{
    public interface IWorldRepository
    {
        IEnumerable<Trip> GetAllTrips();
        IEnumerable<Trip> GetUserTripsWithStops(string name);
        Trip GetTripByName(string tripName, string userName);
        void AddTrip(Trip trip);
        void AddStop(Stop newStop, string userName, string tripName);
        Task<bool> SaveChangesAsync();
    }
}