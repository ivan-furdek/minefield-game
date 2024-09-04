namespace MinefieldGame.Models;

public class Board
{
    public static Position StartPosition { get; } = new(0, 0);
    public static Position EndPosition { get; } = new(Constants.Board.Size - 1, Constants.Board.Size - 1);
    
    public IEnumerable<Mine> Mines { get; }
    public Player Player { get; }
    
    public bool IsPlayerDead => Player.IsDead;
    public bool HasPlayerReachedEnd => Player.Position == EndPosition;

    public Board(IEnumerable<Mine> mines, Player player)
    {
        Mines = mines;
        Player = player;
    }

    public static bool IsStart(Position position) => position == StartPosition;
    public static bool IsEnd(Position position) => position == EndPosition;

    public bool IsPlayerOn(int x, int y) => Player.Position.X == x && Player.Position.Y == y;

    public void MovePlayer(ConsoleKey input)
    {
        var playerPosition = Player.Position;

        var newPosition = input switch
        {
            ConsoleKey.UpArrow =>
                playerPosition with { Y = int.Min(playerPosition.Y + 1, Constants.Board.Size - 1) },
            ConsoleKey.DownArrow =>
                playerPosition with { Y = int.Max(playerPosition.Y - 1, 0) },
            ConsoleKey.RightArrow =>
                playerPosition with { X = int.Min(playerPosition.X + 1, Constants.Board.Size - 1) },
            ConsoleKey.LeftArrow =>
                playerPosition with { X = int.Max(playerPosition.X - 1, 0) },
            _ => throw new ArgumentOutOfRangeException(nameof(input), input, null)
        };

        Player.Move(newPosition);

        var steppedOnMine = Mines.FirstOrDefault(mine => !mine.Exploded && mine.Position == Player.Position);

        if (steppedOnMine != null)
        {
            Player.LoseLife();
            steppedOnMine.Explode();
        }
    }
}