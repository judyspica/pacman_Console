using System;

namespace pacmanj
{
    public class Position
    {
        public Position(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public int X
        {
            get; set;
        }

        public int Y
        {
            get; set;
        }

        public bool CollidesWith(Position position)
        {
            if (X == position.X)
            {
                return 2 > Math.Abs(Y - position.Y);
            }
            else if (Y == position.Y)
            {
                return 2 > Math.Abs(X - position.X);
            }
            else
            {
                return (2 > Math.Abs(Y - position.Y)) && (2 > Math.Abs(X - position.X));
            }
        }
    }
}
