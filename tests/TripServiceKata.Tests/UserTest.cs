using Xunit;

namespace TripServiceKata.Tests;

public class UserTest
{
    [Fact]
    public void HasFriend_WhenUserIsFriend_ReturnsTrue()
    {
        // Arrange
        var batman = new User.User();
        var alfred = new User.User();
        batman.AddFriend(alfred);

        // Act
        var result = batman.HasFriend(alfred);

        // Assert
        Assert.True(result);
    }
    
    [Fact]
    public void HasFriend_WhenUserIsNotFriend_ReturnsFalse()
    {
        // Arrange
        var batman = new User.User();
        var alfred = new User.User();
        var joker = new User.User();
        batman.AddFriend(alfred);
        
        // Act
        var result = batman.HasFriend(joker);
        
        // Assert
        Assert.False(result);
    }
}