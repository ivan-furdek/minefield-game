using MinefieldGame.Interfaces;
using MinefieldGame.Models;

namespace MinefieldGame.Factories;

public class BoardFactory
{
    private readonly IRandomGenerator _randomGenerator;

    public BoardFactory(IRandomGenerator randomGenerator)
    {
        _randomGenerator = randomGenerator;
    }

    public Board Create(int mineCount)
    {
        var mines = GenerateRandomPositions(mineCount)
            .Select(position => new Mine(position))
            .ToList();

        return new Board(mines, Player.CreateDefault());
    }

    private HashSet<Position> GenerateRandomPositions(int mineCount)
    {
        var positions = new HashSet<Position>();

        while (positions.Count < mineCount)
        {
            var newPosition = new Position(
                _randomGenerator.Next(0, Constants.Board.Size),
                _randomGenerator.Next(0, Constants.Board.Size)
            );

            if (Board.IsStart(newPosition) || Board.IsEnd(newPosition))
            {
                continue;
            }

            positions.Add(newPosition);
        }

        return positions;
    }
}