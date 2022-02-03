using System;
using System.Threading;

namespace pacmanj
{
    public class Ghost : AbstractDrawable, IDrawbale
    {
        private readonly string centerSymbol;
        private readonly int Speed;

        public Ghost(Position position, Direction directionOfMovement, string centerSymbol, int Speed ) : base(position, directionOfMovement)
        {
            this.centerSymbol = centerSymbol;
            this.Speed = Speed;
            GameRunning = true;

            new Thread(StartMovingAutonomously).Start();
        }

        private void StartMovingAutonomously()
        {
            do
            {
                MoveOneStep();
                Thread.Sleep(Speed);
            } while (GameRunning);
        }

        public void Draw()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            base.Draw(this);
        }

        public override void MoveOneStep()
        {
            base.MoveOneStep();
            base.SetRandomDirectionOfMovement();
        }

        public string GetCenterSymbol()
        {
            return centerSymbol;
        }

        public string GetFrameSymbol()
        {
            return "G";
        }

        public bool GameRunning
        {
            get; set;
        }
    }
}