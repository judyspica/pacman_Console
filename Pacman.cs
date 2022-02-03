using System;

namespace pacmanj
{
    public class Pacman : AbstractDrawable, IDrawbale
    {
        public Pacman(Position position, Direction directionOfMovement) : base(position, directionOfMovement)
        {
        }

        public void Draw()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            base.Draw(this);
        }

        public string GetCenterSymbol()
        {
            return "P";
        }

        public string GetFrameSymbol()
        {
            return "P";
        }
    }
}