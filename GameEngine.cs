using System;
using System.Collections.Generic;
using System.Threading;
using System.Collections.Concurrent;

namespace pacmanj
{
    public class GameEngine
    {
        private static readonly char[] APLABETICAL_CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        private readonly Pacman pacman;
        private readonly ConcurrentBag<Ghost> ghosts;
        private int indexOfNextCharToUse;
        private ConsoleKey? keyPressed;

        public GameEngine()
        {
            indexOfNextCharToUse = 0;

            pacman = new Pacman(new Position(1, 1), Direction.RIGHT);
            ghosts = new ConcurrentBag<Ghost>();
        }

        public void Start()
        {
            StartThreadForReadingUserInputs();
            StartThreadForCreatingGhosts();

            do
            {
                Console.Clear();
                
                pacman.Draw();
                pacman.SetDirectionOfMovement(keyPressed);
                pacman.MoveOneStep();

                new List<Ghost>(ghosts.ToArray()).ForEach(ghost => ghost.Draw());
                Thread.Sleep(200);

            } while (IsGameRunning());

            new List<Ghost>(ghosts.ToArray()).ForEach(ghost => ghost.GameRunning = false);
        }

        private void StartThreadForReadingUserInputs()
        {
            new Thread(ReadUserKeyPresses).Start();
        }

        private void ReadUserKeyPresses()
        {
            bool GameRunning = true;

            do
            {
                keyPressed = Console.ReadKey().Key;
                if (keyPressed == ConsoleKey.Escape)
                {
                    GameRunning = false;
                }
            } while (GameRunning && IsGameRunning());
        }

        private void StartThreadForCreatingGhosts()
        {
            new Thread(new ThreadStart(CreateGhosts)).Start();
        }

        private void CreateGhosts()
        {
            do
            {
                CreateGhost();
                Thread.Sleep(5000);
            } while (IsGameRunning());
        }

        private bool IsGameRunning()
        {
            return !IsKeyPressedEscape() && !IsCollisiotionDectected();
        }

        private bool IsKeyPressedEscape()
        {
            return keyPressed != null && keyPressed == ConsoleKey.Escape;
        }

        private bool IsCollisiotionDectected()
        {
            List<Ghost> foundGhostsWithCollition = new List<Ghost>(ghosts.ToArray())
                 .FindAll(ghost => pacman.GetPosition().CollidesWith(ghost.GetPosition()));
            return foundGhostsWithCollition.Count != 0;
        }

        private void CreateGhost()
        {
            int x = Ghost.random.Next(1, Console.BufferWidth - 1);
            int y = Ghost.random.Next(1, Console.BufferHeight - 1);

            Position postion = new Position(x, y);
            Direction direction = Ghost.RandomMovementDirection();
            string ghostSymbol = APLABETICAL_CHARS[indexOfNextCharToUse].ToString();
            int speed = AbstractDrawable.random.Next(2, 5) * 200;

            Ghost ghost = new Ghost(postion, direction, ghostSymbol, speed);
            ghosts.Add(ghost);

            SetNextCharToUse();
        }

        private void SetNextCharToUse()
        {
            indexOfNextCharToUse++;
            indexOfNextCharToUse %= 26;
        }
    }
}