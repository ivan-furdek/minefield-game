using MinefieldGame.Interfaces;
using MinefieldGame.Services;
using Moq;

namespace MinefieldGame.Tests.Services;

public class GameRunnerTests
{
    private readonly GameRunner _gameRunner;
    private readonly Mock<IGameFactory> _gameFactoryMock;
    private readonly Mock<IConsoleHandler> _consoleHandlerMock;
    private readonly Mock<IGame> _gameMock;

    public GameRunnerTests()
    {
        _consoleHandlerMock = new Mock<IConsoleHandler>();
        _gameFactoryMock = new Mock<IGameFactory>();
        _gameMock = new Mock<IGame>();

        _gameRunner = new GameRunner(_gameFactoryMock.Object, _consoleHandlerMock.Object);
    }

    [Fact]
    public void When_Run_GameIsCreated()
    {
        // Arrange
        _gameFactoryMock
            .Setup(factory => factory.Create())
            .Returns(_gameMock.Object);

        _consoleHandlerMock
            .Setup(handler => handler.GetInput(It.IsAny<String>(), It.IsAny<HashSet<ConsoleKey>>()))
            .Returns(ConsoleKey.N);

        // Act
        _gameRunner.Run();

        // Assert
        _gameFactoryMock.Verify(factory => factory.Create(), Times.Once);
    }

    [Fact]
    public void When_Input_Y_GameIsRepeated()
    {
        // Arrange
        _gameFactoryMock
            .Setup(factory => factory.Create())
            .Returns(_gameMock.Object);

        _consoleHandlerMock
            .SetupSequence(wrapper => wrapper.GetInput(It.IsAny<string>(), It.IsAny<HashSet<ConsoleKey>>()))
            .Returns(ConsoleKey.Y)
            .Returns(ConsoleKey.Y)
            .Returns(ConsoleKey.N);

        // Act
        _gameRunner.Run();

        // Assert
        _gameFactoryMock.Verify(factory => factory.Create(), Times.Exactly(3));
    }

    [Fact]
    public void When_Input_N_Exit()
    {
        // Arrange
        _gameFactoryMock
            .Setup(factory => factory.Create())
            .Returns(_gameMock.Object);

        _consoleHandlerMock
            .SetupSequence(wrapper => wrapper.GetInput(It.IsAny<String>(), It.IsAny<HashSet<ConsoleKey>>()))
            .Returns(ConsoleKey.Y)
            .Returns(ConsoleKey.N);

        // Act
        _gameRunner.Run();

        // Assert
        _consoleHandlerMock.Verify(handler => handler.GetInput(
                It.IsAny<string>(),
                It.IsAny<HashSet<ConsoleKey>>()), Times.Exactly(2)
        );
    }
}