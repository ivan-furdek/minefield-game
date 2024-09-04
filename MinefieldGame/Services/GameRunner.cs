using MinefieldGame.Interfaces;

namespace MinefieldGame.Services;

public class GameRunner(IGameFactory gameFactory, IConsoleHandler consoleHandler)
{
    public void Run()
    {
        while (true)
        {
            consoleHandler.Clear();

            var game = gameFactory.Create();
            game.Play();

            var response = consoleHandler.GetInput(
                "New Game?",
                new HashSet<ConsoleKey> { ConsoleKey.Y, ConsoleKey.N }
            );

            if (response == ConsoleKey.N) break;
        }
    }
}