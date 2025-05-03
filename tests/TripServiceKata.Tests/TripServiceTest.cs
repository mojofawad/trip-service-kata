using System.Collections.Generic;
using TripServiceKata.Exception;
using Xunit;

namespace TripServiceKata.Tests;

public class TripServiceTest
{
    private readonly FakeTripService _tripService = new FakeTripService();

    private readonly User.User _currentUser = new User.User();
    private readonly User.User _queriedUser = new User.User();

    // Test data
    private static readonly Trip.Trip NewYork = new Trip.Trip();
    private static readonly Trip.Trip Tokyo = new Trip.Trip();
    private static readonly User.User KingKong = new User.User();
    private static readonly User.User Godzilla = new User.User();

    [Fact]
    public void GetTripsByUser_WhenUserNotLoggedIn_ThrowsException()
    {
        // Act & Assert
        Assert.Throws<UserNotLoggedInException>(() => _tripService.GetTripsByUser(_queriedUser));
    }

    [Fact]
    public void GetTripsByUser_WhenCurrentUserIsNotFriend_ReturnsEmptyList()
    {
        // Arrange
        _tripService.SetCurrentLoggedInUser(_currentUser);
        var trips = new List<Trip.Trip> { NewYork, Tokyo };
        _tripService.SetTripsToReturn(trips);

        _queriedUser.AddFriend(KingKong);
        _queriedUser.AddFriend(Godzilla);

        _queriedUser.AddTrip(NewYork);
        _queriedUser.AddTrip(Tokyo);

        // Act
        var result = _tripService.GetTripsByUser(_queriedUser);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void GetTripsByUser_WhenCurrentUserIsFriend_ReturnsTrips()
    {
        // Arrange
        _tripService.SetCurrentLoggedInUser(_currentUser);
        var trips = new List<Trip.Trip> { NewYork, Tokyo };
        _tripService.SetTripsToReturn(trips);

        _queriedUser.AddFriend(Godzilla);
        _queriedUser.AddFriend(_currentUser);

        _queriedUser.AddTrip(NewYork);
        _queriedUser.AddTrip(Tokyo);

        // Act
        var result = _tripService.GetTripsByUser(_queriedUser);

        // Assert
        Assert.Equal(_queriedUser.Trips(), result);
    }

    [Fact]
    public void GetTripsByUser_WhenQueriedUserHasNoTrips_ReturnsEmptyList()
    {
        // Arrange
        _tripService.SetCurrentLoggedInUser(_currentUser);
        var trips = new List<Trip.Trip>();
        _tripService.SetTripsToReturn(trips);

        _queriedUser.AddFriend(_currentUser);

        // Act
        var result = _tripService.GetTripsByUser(_queriedUser);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void GetTripsByUser_WhenQueriedUserHasNoFriends_ReturnsEmptyList()
    {
        // Arrange
        _tripService.SetCurrentLoggedInUser(_currentUser);
        var trips = new List<Trip.Trip> { NewYork, Tokyo };
        _tripService.SetTripsToReturn(trips);

        // Act
        var result = _tripService.GetTripsByUser(_queriedUser);

        // Assert
        Assert.Empty(result);
    }
}