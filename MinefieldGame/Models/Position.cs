namespace MinefieldGame.Models;

public record Position(int X, int Y)
{
    public override string ToString()
    {
        return $"{(char)(65 + X)}{Y + 1}";
    }
}