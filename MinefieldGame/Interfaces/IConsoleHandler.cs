namespace MinefieldGame.Interfaces;

public interface IConsoleHandler
{
    ConsoleKey GetInput(string prompt, ISet<ConsoleKey> allowedKeys);

    void WriteLine(string line);

    void WriteLine(object obj);
    
    void Clear();
}