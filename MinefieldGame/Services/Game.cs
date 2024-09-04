using MinefieldGame.Interfaces;
using MinefieldGame.Models;

namespace MinefieldGame.Services;

public class Game(IConsoleHandler consoleHandler, BoardRenderer boardRenderer, Board board) : IGame
{
    public void Play()
    {
        while (true)
        {
            ShowGameState();

            var input = consoleHandler.GetInput(
                "Next move?",
                new HashSet<ConsoleKey>
                {
                    ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.LeftArrow, ConsoleKey.RightArrow
                }
            );

            board.MovePlayer(input);

            if (board.IsPlayerDead)
            {
                ShowGameState();
                consoleHandler.WriteLine("You lose!");
                break;
            }

            if (board.HasPlayerReachedEnd)
            {
                ShowGameState();
                consoleHandler.WriteLine("You win!");
                break;
            }
        }
    }

    private void ShowGameState()
    {
        consoleHandler.Clear();
        boardRenderer.RenderBoard(consoleHandler, board);
        consoleHandler.WriteLine(board.Player);
    }
}