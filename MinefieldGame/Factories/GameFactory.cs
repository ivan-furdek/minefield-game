using MinefieldGame.Interfaces;
using MinefieldGame.Models;
using MinefieldGame.Services;

namespace MinefieldGame.Factories;

public class GameFactory(IConsoleHandler consoleHandler, BoardRenderer boardRenderer, BoardFactory boardFactory)
    : IGameFactory
{
    public IGame Create() => new Game(consoleHandler, boardRenderer, boardFactory.Create(Constants.Board.MineCount));
}