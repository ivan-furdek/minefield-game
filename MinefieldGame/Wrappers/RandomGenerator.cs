using MinefieldGame.Interfaces;

namespace MinefieldGame.Wrappers;

public class RandomGenerator(Random random) : IRandomGenerator
{
    public int Next(int min, int max) => random.Next(min, max);
}