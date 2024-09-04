using MinefieldGame.Interfaces;

namespace MinefieldGame.Wrappers;

public class ConsoleHandler : IConsoleHandler
{
    public ConsoleKey GetInput(string prompt, ISet<ConsoleKey> allowedKeys)
    {
        var allowedInputs = allowedKeys
            .Select(key => key.ToString())
            .Aggregate((key1, key2) => key1.ToString() + "/" + key2.ToString());

        ConsoleKey response;

        do
        {
            Console.Write($"{prompt} [{allowedInputs}]: ");

            response = Console.ReadKey().Key;

            if (response != ConsoleKey.Enter)
                Console.WriteLine();
        } while (!allowedKeys.Contains(response));

        return response;
    }

    public void WriteLine(string line)
    {
        Console.WriteLine(line);
    }

    public void WriteLine(object obj)
    {
        Console.WriteLine(obj);
    }

    public void Clear()
    {
        Console.Clear();
        Console.WriteLine("\x1b[3J");
        Console.Clear();
    }
}