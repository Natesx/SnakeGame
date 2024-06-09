using SnakeGame.Enumes;
using SnakeGame.Models;
using System.Xml.Linq;

namespace SnakeGame.Helpers
{
    public static class Utilities
    {
        public static void WriteOptionsSelect(List<Option> options, Option selectedOption, string title)
        {
            Console.Clear();
            Console.WriteLine(title);

            foreach (Option option in options)
            {
                if (option == selectedOption) Console.Write("> ");
                else Console.Write(" ");

                Console.WriteLine(option.Name);
            }
        }

        public static void DrawBoundaries(int arenaHeight, int arenaWidth)
        {
            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < arenaWidth; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("█");
                Console.SetCursorPosition(i, arenaHeight - 1);
                Console.Write("█");
            }
            for (int i = 0; i < arenaHeight; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("██");
                Console.SetCursorPosition(arenaWidth - 2, i);
                Console.Write("██");
            }
        }

        public static void DrawSnake(Snake snake)
        {
            foreach (var segment in snake.SnakeBody)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.SetCursorPosition(segment.X, segment.Y);
                Console.Write("██");
            }
            Console.ResetColor();
        }

        public static bool MoveSnake(Snake snake, Direction direction)
        {
            Pixel newHeadPosition = new Pixel()
            {
                X = snake.SnakeBody[0].X,
                Y = snake.SnakeBody[0].Y,
            };

            switch (direction)
            {
                case Direction.Up: newHeadPosition.Y--; break;
                case Direction.Down: newHeadPosition.Y++; break;
                case Direction.Left: newHeadPosition.X = newHeadPosition.X - 2; break;
                case Direction.Right: newHeadPosition.X = newHeadPosition.X + 2; break;
            }

            if (snake.SnakeBody.Exists(x => x.X == newHeadPosition.X && x.Y == newHeadPosition.Y))
            {
                return true;
            }

            snake.SnakeBody.Insert(0, newHeadPosition);
            return false;
        }

        public static void DrawScores(int scores, int arenaHeight)
        {
            Console.SetCursorPosition(0, arenaHeight + 1);
            Console.Write("Punkty: " + scores);
        }

        public static void DrawFood(Snake snake, ref Pixel? food, int arenaHeight, int arenaWidth)
        {
            Random random = new Random();

            if (food == null)
            {
                Pixel newFood = new Pixel();
                newFood.X = random.Next(2, arenaWidth - 2);
                newFood.Y = random.Next(1, arenaHeight - 1);

                while (snake.SnakeBody.Exists(snakeBody =>
                    (snakeBody.X == newFood.X && snakeBody.Y == newFood.Y))
                    || newFood.X % 2 != 0)
                {
                    newFood.X = random.Next(2, arenaWidth - 2);
                    newFood.Y = random.Next(1, arenaHeight - 1);
                }

                food = newFood;
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(food.X, food.Y);
            Console.Write("██");

            Console.ResetColor();
        }

        public static bool CollisionWithBoundaries(Snake snake, int arenaWidth, int arenaHeight)
        {
            if (snake.SnakeBody[0].X == 0 || snake.SnakeBody[0].X == arenaWidth - 2
                || snake.SnakeBody[0].Y == 0 || snake.SnakeBody[0].Y == arenaHeight - 1)
            {
                return true;
            }

            return false;
        }

        public static void CheckFoodEaten(Snake snake, ref Pixel? food, ref int scores)
        {
            Pixel snakeHead = snake.SnakeBody[0];

            if (snakeHead.X == food.X && snakeHead.Y == food.Y)
            {
                scores++;
                food = null;
            }
            else
            {
                Pixel removeTile = snake.SnakeBody[snake.SnakeBody.Count - 1];
                Console.SetCursorPosition(removeTile.X, removeTile.Y);
                Console.Write("  ");

                snake.SnakeBody.RemoveAt(snake.SnakeBody.Count - 1);
            }
        }

        public static void GameOver(int arenaWidth, int arenaHeight, int score)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(arenaWidth / 5, 1);
            Console.WriteLine("Koniec gry!");
            Console.SetCursorPosition(arenaWidth / 5, 2);
            Console.WriteLine("Twój wynik: " + score);

            for (int i = 0; i < 3; i++)
            {
                Console.SetCursorPosition(arenaWidth / 5, 3);
                Console.WriteLine("                                               ");
                Console.SetCursorPosition(arenaWidth / 5, 4);
                Console.WriteLine("Powrót do menu za " + (3 - i));
                Thread.Sleep(1000);
            }

            Console.ResetColor();
        }
    }
}