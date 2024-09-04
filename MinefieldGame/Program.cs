using Microsoft.Extensions.DependencyInjection;
using MinefieldGame.Factories;
using MinefieldGame.Interfaces;
using MinefieldGame.Services;
using MinefieldGame.Wrappers;

namespace MinefieldGame;

class Program
{
    static void Main(string[] args)
    {
        var services = CreateServices();

        var game = services.GetRequiredService<GameRunner>();
        game.Run();
    }

    private static ServiceProvider CreateServices()
    {
        var serviceProvider = new ServiceCollection()
            .AddSingleton<GameRunner>()
            .AddSingleton<IConsoleHandler, ConsoleHandler>()
            .AddSingleton<IRandomGenerator>(_ => new RandomGenerator(new Random()))
            .AddSingleton<BoardRenderer>()
            .AddSingleton<BoardFactory>()
            .AddSingleton<IGameFactory, GameFactory>()
            .BuildServiceProvider();

        return serviceProvider;
    }
}