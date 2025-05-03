using System.Collections.Generic;
using TripServiceKata.Trip;

namespace TripServiceKata.Tests
{
    public class FakeTripService : TripService
    {
        private User.User _currentLoggedInUser;
        private List<Trip.Trip> _tripsToReturn = new List<Trip.Trip>();
        
        public void SetTripsToReturn(List<Trip.Trip> trips)
        {
            _tripsToReturn = trips;
        }

        protected override List<Trip.Trip> GetTripsForQueriedUser(User.User queriedUser)
        {
            return _tripsToReturn;
        }
        public void SetCurrentLoggedInUser(User.User user)
        {
            _currentLoggedInUser = user;
        }

        protected override User.User GetCurrentLoggedInUser()
        {
            return _currentLoggedInUser;
        }
    }
}