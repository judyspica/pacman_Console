using System;

namespace pacmanj
{
    public abstract class AbstractDrawable
    {
        public static readonly Random random = new Random();

        private Position position;
        private Direction directionOfMovement;

        protected AbstractDrawable(Position position, Direction directionOfMovement)
        {
            this.position = position;
            this.directionOfMovement = directionOfMovement;
        }

        public void Draw(IDrawbale drawbale)
        {
            string frameSymbole = drawbale.GetFrameSymbol();
            string centerSymbole = drawbale.GetCenterSymbol();
            try
            {
                Console.SetCursorPosition(position.X - 1, position.Y - 1);
                Console.WriteLine(frameSymbole + frameSymbole + frameSymbole);
                Console.SetCursorPosition(position.X - 1, position.Y);
                Console.WriteLine(frameSymbole + centerSymbole + frameSymbole);
                Console.SetCursorPosition(position.X - 1, position.Y + 1);
                Console.WriteLine(frameSymbole + frameSymbole + frameSymbole);

            }
            catch (ArgumentOutOfRangeException) { }
        }

        public void SetDirectionOfMovement(ConsoleKey? keyPressed)
        {
            if (keyPressed != null)
            {
                switch (keyPressed)
                {
                    case ConsoleKey.UpArrow:
                        directionOfMovement = Direction.UP;
                        break;
                    case ConsoleKey.DownArrow:
                        directionOfMovement  = Direction.DOWN;
                        break;
                    case ConsoleKey.RightArrow:
                        directionOfMovement = Direction.RIGHT;
                        break;
                    case ConsoleKey.LeftArrow:
                        directionOfMovement = Direction.LEFT;
                        break;
                    default:
                        break;
                }
            }
        }

        public virtual void MoveOneStep()
        {
            switch (directionOfMovement)
            {
                case Direction.DOWN:
                    position.Y += 3;
                    break;
                case Direction.UP:
                    position.Y -= 3;
                    break;
                case Direction.RIGHT:
                    position.X += 3;
                    break;
                case Direction.LEFT:
                    position.X -= 3;
                    break;
            }

            SkipWall();
        }

        private void SkipWall()
        {
            if (position.X + 1 > Console.BufferWidth )
            {
                position.X = 1;
            }
            else if (position.X - 1 < 0)
            {
                position.X = Console.BufferWidth - 1;
            }

            if (position.Y + 1 > Console.BufferHeight )
            {
                position.Y = 1;
            }
            else if (position.Y - 1 < 0)
            {
                position.Y = Console.BufferHeight - 1;
            }
        }

        public void SetRandomDirectionOfMovement()
        {
            directionOfMovement = RandomMovementDirection();
        }

        public static Direction RandomMovementDirection()
        {
            return (Direction) random.Next(0, 4);
        }

        public Position GetPosition()
        {
            return position;
        }
    }
}
