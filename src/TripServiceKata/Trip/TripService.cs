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

            
            var friends = queriedUser.GetFriends();
            var tripList = new List<Trip>();
            
            var isFriend = false;
            foreach (User.User friend in friends)
            {
                if (friend.Equals(currentLoggedInUser))
                {
                    isFriend = true;
                    break;
                }
            }

            if (isFriend)
            {
                tripList = GetTripsForQueriedUser(queriedUser);
            }

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
