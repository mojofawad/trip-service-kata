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
            User.User currentLoggedInUser = UserSession.GetInstance().GetLoggedUser();
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
                    tripList = TripDAO.FindTripsByUser(queriedUser);
                }
                return tripList;
            }
            else
            {
                throw new UserNotLoggedInException();
            }
        }
    }
}
