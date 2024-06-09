using SnakeGame;
using SnakeGame.Helpers;
using SnakeGame.Models;

int index = 0;
List<Option> options = new List<Option>()
{
    new Option("Start", () => Game.Run(new GameConfig())), 
    new Option("Ustawienia gry", () => Console.WriteLine("\nWybrano 'Ustawienia gry'")), 
    new Option("Zakończ grę", () => Environment.Exit(0)) 
};

Utilities.WriteOptionsSelect(options, options[index], "Używając strzałek wybierz opcję:\n");

ConsoleKeyInfo keyinfo;
do
{
    keyinfo = Console.ReadKey();

    if (keyinfo.Key == ConsoleKey.DownArrow && index + 1 < options.Count) index++;
    else if (keyinfo.Key == ConsoleKey.UpArrow && index - 1 >= 0) index--;
    else if (keyinfo.Key == ConsoleKey.Enter) options[index].Selected.Invoke();

    Utilities.WriteOptionsSelect(options, options[index], "Używając strzałek wybierz opcję:\n");
} while (true);