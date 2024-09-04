using MinefieldGame.Interfaces;
using MinefieldGame.Models;
using MinefieldGame.Services;
using Moq;

namespace MinefieldGame.Tests.Services;

public class GameTests
{
    private readonly Mock<IConsoleHandler> _consoleHandlerMock;
    private Game _game;

    public GameTests()
    {
        _consoleHandlerMock = new Mock<IConsoleHandler>();
    }

    [Fact]
    public void When_PlayerLosesAllLives_ExitGameLoop()
    {
        // Arrange
        var mines = new HashSet<Mine>
        {
            new(new Position(0, 1)),
            new(new Position(0, 2)),
            new(new Position(0, 3)),
        };

        var player = Player.CreateDefault();
        var board = new Board(mines, player);

        _game = new Game(
            _consoleHandlerMock.Object,
            new BoardRenderer(),
            board
        );

        _consoleHandlerMock
            .SetupSequence(handler => handler.GetInput(It.IsAny<string>(), It.IsAny<HashSet<ConsoleKey>>()))
            .Returns(ConsoleKey.UpArrow)
            .Returns(ConsoleKey.UpArrow)
            .Returns(ConsoleKey.UpArrow);
        
        // Act
        _game.Play();
        
        // Assert
        Assert.True(board.IsPlayerDead);
    }
    
    [Fact]
    public void When_PlayerReachesEnd_ExitGameLoop()
    {
        // Arrange
        var mines = new HashSet<Mine>();

        var player = Player.CreateDefault();
        var board = new Board(mines, player);

        _game = new Game(
            _consoleHandlerMock.Object,
            new BoardRenderer(),
            board
        );

        _consoleHandlerMock
            .SetupSequence(handler => handler.GetInput(It.IsAny<string>(), It.IsAny<HashSet<ConsoleKey>>()))
            .Returns(ConsoleKey.UpArrow)
            .Returns(ConsoleKey.UpArrow)
            .Returns(ConsoleKey.UpArrow)
            .Returns(ConsoleKey.UpArrow)
            .Returns(ConsoleKey.UpArrow)
            .Returns(ConsoleKey.UpArrow)
            .Returns(ConsoleKey.UpArrow)
            .Returns(ConsoleKey.RightArrow)
            .Returns(ConsoleKey.RightArrow)
            .Returns(ConsoleKey.RightArrow)
            .Returns(ConsoleKey.RightArrow)
            .Returns(ConsoleKey.RightArrow)
            .Returns(ConsoleKey.RightArrow)
            .Returns(ConsoleKey.RightArrow)
            .Returns(ConsoleKey.RightArrow);
        
        // Act
        _game.Play();
        
        // Assert
        Assert.True(board.HasPlayerReachedEnd);
    }
}