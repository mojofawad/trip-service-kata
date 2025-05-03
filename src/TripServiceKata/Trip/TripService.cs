using System.Collections.Generic;
using TripServiceKata.Exception;
using TripServiceKata.User;

namespace TripServiceKata.Trip
{
    public class TripService
    {
        public List<Trip> GetTripsByUser(User.User queriedUser)
        {
            User.User currentLoggedInUser = GetCurrentLoggedInUser();
            if (currentLoggedInUser == null)
            {
                throw new UserNotLoggedInException();
            }

            bool isFriend = false;
            foreach (User.User friend in queriedUser.GetFriends())
            {
                if (friend.Equals(currentLoggedInUser))
                {
                    isFriend = true;
                    break;
                }
            }

            List<Trip> tripList = new List<Trip>();
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
