using MinefieldGame.Models;

namespace MinefieldGame.Tests.Models;

public class PlayerTests
{
    [Fact]
    public void When_NoLivesLeft_PlayerIsDead()
    {
        // Arrange
        var player = Player.CreateDefault();

        // Act
        player.LoseLife();
        player.LoseLife();
        player.LoseLife();

        // Assert
        Assert.True(player.IsDead);
    }
    
    [Fact]
    public void When_MovingToSamePosition_NoMovesAreMade()
    {
        // Arrange
        var player = Player.CreateDefault();
        
        // Act
        player.Move(new Position(0,0));

        // Assert
        Assert.Equal(0, player.MovesTaken);
    }
}