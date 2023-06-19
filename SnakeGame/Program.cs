using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SnakeGame
{
    internal class Snake
    {
        static ConsoleKeyInfo keyInfo; // Keyboard input
        public static int size = 20; // Size of the square board
        // Create the board
        public static char[,] board = new char[size, size];

        // Set the snake's body
        public static int snakeLength = 3; // Initial length of the snake
        public static int snakeX = size / 2; // Initial X coordinate of the snake's head
        public static int snakeY = size / 2; // Initial Y coordinate of the snake's head
        static int[] tailX = new int[size * size]; // X coordinates of the snake's tail
        static int[] tailY = new int[size * size]; // Y coordinates of the snake's tail
        static int tailLength; // Length of the snake's tail
        static bool gameover; // Game over flag
        static int fruitX; // X coordinate of the fruit
        static int fruitY; // Y coordinate of the fruit

        static void Main(string[] args)
        {
            InitializeGame();

            while (!gameover)
            {
                if (Console.KeyAvailable)
                {
                    keyInfo = Console.ReadKey(true);
                    HandleInput();
                }

                Update(); // Update the snake's tail positions
                MostrarTablero(); // Display the updated board

                Thread.Sleep(100);
            }
        }

        static void InitializeGame()
        {
            gameover = false;
            tailLength = 0;
            GenerateFruit(); // Generate the initial fruit

            MostrarTablero(); // Display the initial board
        }

        static void MostrarTablero()
        {
            Console.Clear();

            // Initialize the board with empty spaces
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    board[row, col] = ' ';
                }
            }

            // Set the borders of the board
            for (int row = 0; row < size; row++)
            {
                board[row, 0] = '#'; // Left border
                board[row, size - 1] = '#'; // Right border
            }

            for (int col = 0; col < size; col++)
            {
                board[0, col] = '#'; // Top border
                board[size - 1, col] = '#'; // Bottom border
            }

            // Set the snake's head position
            board[snakeY, snakeX] = '*';

            // Set the snake's tail positions
            for (int i = 0; i < tailLength; i++)
            {
                board[tailY[i], tailX[i]] = '*';
            }

            // Set the fruit position
            board[fruitY, fruitX] = '@';

            // Display the board
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    Console.Write(board[row, col] + " ");
                }
                Console.WriteLine();
            }
        }

        static void HandleInput()
        {
            switch (keyInfo.Key)
            {
                case ConsoleKey.LeftArrow:
                    if (tailLength == 0 || snakeX > tailX[0])
                    {
                        snakeX--;
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (tailLength == 0 || snakeX < tailX[0])
                    {
                        snakeX++;
                    }
                    break;
                case ConsoleKey.UpArrow:
                    if (tailLength == 0 || snakeY > tailY[0])
                    {
                        snakeY--;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (tailLength == 0 || snakeY < tailY[0])
                    {
                        snakeY++;
                    }
                    break;
                case ConsoleKey.Escape:
                    gameover = true;
                    break;
            }
        }

        static void Update()
        {
            // Check if the snake's head collides with the fruit
            if (snakeX == fruitX && snakeY == fruitY)
            {
                // Generate a new fruit
                GenerateFruit();

                // Enlarge the snake
                tailLength++;
            }

            // Update tail positions
            if (tailLength > 0)
            {
                int prevX = tailX[0];
                int prevY = tailY[0];
                int tempX, tempY;

                tailX[0] = snakeX;
                tailY[0] = snakeY;

                for (int i = 1; i < tailLength; i++)
                {
                    tempX = tailX[i];
                    tempY = tailY[i];

                    tailX[i] = prevX;
                    tailY[i] = prevY;

                    prevX = tempX;
                    prevY = tempY;
                }
            }
        }

        static void GenerateFruit()
        {
            // Generate random coordinates for the fruit within the game board
            Random random = new Random();
            fruitX = random.Next(1, size - 1);
            fruitY = random.Next(1, size - 1);
        }
    }
}
