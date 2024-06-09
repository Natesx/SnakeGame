using SnakeGame.Enumes;
using SnakeGame.Helpers;
using SnakeGame.Models;
using System.Xml.Linq;

namespace SnakeGame
{
    public static class Game
    {
        public static void Run(GameConfig gameConfig)
        {
            Console.Clear();
            Utilities.DrawBoundaries(gameConfig.AreaHeight, gameConfig.AreaWidth);

            Snake snake = new Snake(gameConfig.AreaWidth, gameConfig.AreaHeight);
            Direction direction = Direction.Left;
            Pixel? food = null;
            int scores = 0;
            bool quiet = false;

            while (!quiet)
            {
                Utilities.DrawScores(scores, gameConfig.AreaHeight);
                Utilities.DrawFood(snake, ref food, gameConfig.AreaHeight, gameConfig.AreaWidth);

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo info = Console.ReadKey(true);
                    switch (info.Key)
                    {
                        case ConsoleKey.UpArrow:
                            if (direction != Direction.Down) direction = Direction.Up;
                            break;

                        case ConsoleKey.DownArrow:
                            if (direction != Direction.Up) direction = Direction.Down;
                            break;

                        case ConsoleKey.LeftArrow:
                            if (direction != Direction.Right) direction = Direction.Left;
                            break;

                        case ConsoleKey.RightArrow:
                            if (direction != Direction.Left) direction = Direction.Right;
                            break;

                        case ConsoleKey.Escape:
                            quiet = true;
                            break;
                    }
                }

                if (Utilities.MoveSnake(snake, direction) || Utilities.CollisionWithBoundaries(snake, gameConfig.AreaWidth, gameConfig.AreaHeight)) break;

                Utilities.DrawSnake(snake);
                Utilities.CheckFoodEaten(snake, ref food, ref scores);

                Console.SetCursorPosition(0, 0);
                Thread.Sleep(100);
            }

            Utilities.GameOver(gameConfig.AreaHeight, gameConfig.AreaWidth, scores);
        }
    }
}