using MinefieldGame.Models;

namespace MinefieldGame.Tests.Models;

public class BoardTests
{
    [Fact]
    public void When_MovePlayer_PlayerPositionIsUpdated()
    {
        // Arrange
        var mines = Enumerable.Empty<Mine>();

        var player = Player.CreateDefault();
        var board = new Board(mines, player);

        // Act
        board.MovePlayer(ConsoleKey.UpArrow);

        // Assert
        Assert.Equal(1, player.Position.Y);
    }

    [Fact]
    public void When_MovePlayer_MoveOnMine_MineIsExploded()
    {
        // Arrange
        var mines = new List<Mine>()
        {
            new(new Position(0, 1))
        };

        var player = Player.CreateDefault();
        var board = new Board(mines, player);

        // Act
        board.MovePlayer(ConsoleKey.UpArrow);

        // Assert
        Assert.True(mines.First().Exploded);
    }

    [Fact]
    public void When_MovePlayer_MoveOnMine_PlayerLosesLife()
    {
        // Arrange
        var mines = new List<Mine>()
        {
            new(new Position(0, 1))
        };

        var player = Player.CreateDefault();
        var board = new Board(mines, player);

        // Act
        board.MovePlayer(ConsoleKey.UpArrow);

        // Assert
        Assert.Equal(Constants.Player.Lives - 1, player.Lives);
    }
    
    [Fact]
    public void When_MovePlayer_MoveOnExplodedMine_PlayerDoesNot_LoseLife()
    {
        // Arrange
        var mines = new List<Mine>()
        {
            new(new Position(0, 1))
        };

        mines.First().Explode();

        var player = Player.CreateDefault();
        var board = new Board(mines, player);

        // Act
        board.MovePlayer(ConsoleKey.UpArrow);

        // Assert
        Assert.Equal(Constants.Player.Lives, player.Lives);
    }

    [Fact]
    public void When_MovePlayer_PlayerNearEdge_CantLeaveMap_Down()
    {
        // Arrange
        var mines = Enumerable.Empty<Mine>();

        var player = Player.CreateDefault();
        var board = new Board(mines, player);

        // Act
        board.MovePlayer(ConsoleKey.DownArrow);

        // Assert
        Assert.Equal(0, player.Position.Y);
    }

    [Fact]
    public void When_MovePlayer_PlayerNearEdge_CantLeaveMap_Left()
    {
        // Arrange
        var mines = Enumerable.Empty<Mine>();

        var player = Player.CreateDefault();
        var board = new Board(mines, player);

        // Act
        board.MovePlayer(ConsoleKey.LeftArrow);

        // Assert
        Assert.Equal(0, player.Position.X);
    }

    [Fact]
    public void When_MovePlayer_PlayerNearEdge_CantLeaveMap_Up()
    {
        // Arrange
        var mines = Enumerable.Empty<Mine>();

        var player = Player.CreateDefault();
        player.Move(new Position(0, Constants.Board.Size - 1));

        var board = new Board(mines, player);

        // Act
        board.MovePlayer(ConsoleKey.UpArrow);

        // Assert
        Assert.Equal(Constants.Board.Size - 1, player.Position.Y);
    }

    [Fact]
    public void When_MovePlayer_PlayerNearEdge_CantLeaveMap_Right()
    {
        // Arrange
        var mines = Enumerable.Empty<Mine>();

        var player = Player.CreateDefault();
        player.Move(new Position(Constants.Board.Size - 1, 0));

        var board = new Board(mines, player);

        // Act
        board.MovePlayer(ConsoleKey.RightArrow);

        // Assert
        Assert.Equal(Constants.Board.Size - 1, player.Position.X);
    }
}