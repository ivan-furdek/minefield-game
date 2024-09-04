using System.Text;
using MinefieldGame.Interfaces;
using MinefieldGame.Models;

namespace MinefieldGame.Services;

public class BoardRenderer
{
    public void RenderBoard(IConsoleHandler consoleHandler, Board board)
    {
        var boardSize = Constants.Board.Size;

        var mineLocations = board.Mines.ToDictionary(
            mine => mine.Position.X * boardSize + mine.Position.Y,
            mine => mine
        );

        var row = new StringBuilder();

        var separatorRow = BuildSeparatorRow(row, boardSize);
        consoleHandler.WriteLine(separatorRow);
        row.Clear();

        for (var i = boardSize - 1; i >= 0; i--)
        {
            row.Append($"{i + 1} |");

            for (var j = 0; j <= boardSize - 1; j++)
            {
                if (board.IsPlayerOn(j, i))
                {
                    row.Append(" O |");
                    continue;
                }

                if (mineLocations.TryGetValue(i + j * boardSize, out var mine) && mine.Exploded)
                {
                    row.Append(" X |");
                }
                else
                {
                    row.Append("   |");
                }
            }

            consoleHandler.WriteLine(row.ToString());
            row.Clear();

            consoleHandler.WriteLine(separatorRow);
        }

        BuildLetterRow(row, boardSize);
        consoleHandler.WriteLine(row.ToString());
    }

    private static string BuildSeparatorRow(StringBuilder row, int boardSize)
    {
        row.Append("  -");
        foreach (var i in Enumerable.Range(0, boardSize))
        {
            row.Append($"----");
        }
        
        return row.ToString();
    }
    
    private static void BuildLetterRow(StringBuilder row, int boardSize)
    {
        row.Append(' ');
        foreach (var i in Enumerable.Range(0, boardSize))
        {
            row.Append($"   {(char)(65 + i)}");
        }

        row.Append('\n');
    }
}