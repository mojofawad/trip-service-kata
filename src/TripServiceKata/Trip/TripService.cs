using System.Collections.Generic;
using TripServiceKata.Exception;
using TripServiceKata.User;

namespace TripServiceKata.Trip
{
    public class TripService
    {
        public List<Trip> GetTripsByUser(User.User queriedUser)
        {
            List<Trip> tripList = new List<Trip>();
            User.User currentLoggedInUser = GetCurrentLoggedInUser();
            bool isFriend = false;
            if (currentLoggedInUser != null)
            {
                foreach(User.User friend in queriedUser.GetFriends())
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
            else
            {
                throw new UserNotLoggedInException();
            }
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
