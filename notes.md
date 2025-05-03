# Notes

## Preparatory Notes

```csharp
using System.Collections.Generic;
using TripServiceKata.Exception;
using TripServiceKata.User;

namespace TripServiceKata.Trip
{
    public class TripService
    {
        // Get a list of trips for a specific user (**REQUESTED** user)
        public List<Trip> GetTripsByUser(User.User user)
        {
            // Initialize an empty list of trips
            List<Trip> tripList = new List<Trip>();
            
            // Check if the **CURRENT** user is logged in 
            User.User loggedUser = UserSession.GetInstance().GetLoggedUser(); // NOTE: this user is different than the **REQUESTED** user
            
            // Initialize a boolean isFriend
            bool isFriend = false;
            
            // Verify the **CURRENT** user is logged in
            if (loggedUser != null)
            {
                // Iterate over a list of Users in the **REQUESTED** user's friend list
                foreach(User.User friend in user.GetFriends())
                {
                    // Check if the friend is the same as the logged in user
                    if (friend.Equals(loggedUser))
                    {
                        // Set isFriend to true
                        isFriend = true;
                        break;
                    }
                }
                
                // If the **CURRENT** user is a friend of the **REQUESTED** user
                if (isFriend)
                {
                    // Get the list of trips for the **REQUESTED** user
                    tripList = TripDAO.FindTripsByUser(user);
                }
                return tripList;
            }
            // If the **CURRENT** user is not logged in
            else
            {
                // Throw an exception
                throw new UserNotLoggedInException();
            }
        }
    }
}
```

### Code Walkthrough

- `GetTripsByUser` is called to retrieve a list of trips for the specific `user` passed as a parameter.
  - This is the **REQUESTED** user.
- Verify that the request is being made by a `user` that is logged in
  - This is the **CURRENT** user.
  - If the **CURRENT** user is not logged in:
    - throw an exception and stop the execution. No trips will be returned.
  - If the **CURRENT** user is logged in:
    - Retrieve and iterate over the list of friends for the **REQUESTED** user.
      - If the **CURRENT** user is a friend of the **REQUESTED** user:
        - set `isFriend` to true
        - break out of the loop
    - If the **CURRENT** user is a friend of the **REQUESTED** user:
      - retrieve and return the list of trips for the **REQUESTED** user
    - If the **CURRENT** user is not a friend of the **REQUESTED** user:
      - return an empty list of trips

> #### Requirements from the README.md
> - Imagine a social networking website for travelers:
> - You need to be logged in to see the content
> - You need to be a friend to see someone else's trips
> - If you are not logged in, the code throws an exception

### Initial thoughts
- Organization needed prior to writing tests
- Checking through the list of **REQUESTED** user's friends threw me off a bit.
  - There might be a reason for this, so I won't question it for now.
- I will start by organizing some of the code to make it easier to read without refactoring.
- Once the code is organized better, I will start writing tests.
  - I am aiming to make as few automated refactors as possible before writing the tests.

### Thoughts while organizing
- I felt as if `CurrentUser` was not clear enough, so I opted for `CurrentLoggedInUser` to make it clear that this user making the request needs to be logged in
  - I'm not sure how a user would be logged out to make the request, but that's not a concern right now
- I also felt as if `RequestedUser` was not clear enough, so I opted for `queriedUser` to make it clear that this user is being queried for trips

