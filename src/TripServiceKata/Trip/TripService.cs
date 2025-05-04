using System.Collections.Generic;
using System.Linq;
using TripServiceKata.Exception;
using TripServiceKata.User;

namespace TripServiceKata.Trip
{
    public class TripService
    {
        public List<Trip> GetTripsByUser(User.User queriedUser)
        {
            var currentLoggedInUser = GetCurrentLoggedInUser();

            if (currentLoggedInUser == null)
            {
                throw new UserNotLoggedInException();
            }
            
            var tripList = queriedUser.HasFriend(currentLoggedInUser) 
                ? GetTripsForQueriedUser(queriedUser) 
                : new List<Trip>();

            return tripList;
        }

        protected virtual List<Trip> GetTripsForQueriedUser(User.User queriedUser)
        {
            return TripDAO.FindTripsByUser(queriedUser);
        }

        protected virtual User.User GetCurrentLoggedInUser()
        {
            return UserSession.GetInstance().GetLoggedUser();
        }
    }
}