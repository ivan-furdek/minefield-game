namespace MinefieldGame.Models;

public class Mine
{
    public Position Position { get; }
    public bool Exploded { get; private set; }

    public Mine(Position position)
    {
        Position = position;
    }

    public void Explode()
    {
        Exploded = true;
    }
}