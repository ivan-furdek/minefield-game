namespace MinefieldGame.Models;

public class Player
{
    public int Lives { get; private set; }
    public Position Position { get; private set; }
    public int MovesTaken { get; private set; }

    private Player(int lives, Position position)
    {
        Lives = lives;
        Position = position;
    }

    public static Player CreateDefault() => new(Constants.Player.Lives, Board.StartPosition);

    public void LoseLife() => Lives--;

    public bool IsDead => Lives <= 0;

    public override string ToString()
    {
        return $"Lives: {Lives}\n" +
               $"Position: {Position}\n" +
               $"Moves Taken: {MovesTaken}\n";
    }

    public void Move(Position newPosition)
    {
        if (Position == newPosition) return;

        Position = newPosition;
        MovesTaken++;
    }
}