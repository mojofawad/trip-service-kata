﻿using System.Collections.Generic;
using System.Linq;

namespace TripServiceKata.User
{
    public class User
    {
        private List<Trip.Trip> trips = new List<Trip.Trip>();
        private List<User> friends = new List<User>();

        public List<User> GetFriends()
        {
            return friends;
        } 

        public void AddFriend(User user)
        {
            friends.Add(user);
        }

        public void AddTrip(Trip.Trip trip)
        {
            trips.Add(trip);
        }

        public List<Trip.Trip> Trips()
        {
            return trips;
        }

        public bool HasFriend(User user)
        {
            return friends.Any(friend => friend == user);
        }
    }
}
