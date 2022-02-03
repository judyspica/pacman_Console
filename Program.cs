using System;

namespace pacmanj
{
    public class Program
    {
        private static readonly int BLOCK_SIZE = 3;
        private static readonly int MIN_DIMENTION_LENGTH = 6;

        public static void Main()
        {
            SetupConsolDisplay();
            new GameEngine().Start();
        }

        private static void SetupConsolDisplay()
        {
            int width = ReadValidValue("Enter desired width (max value {0}): ", Console.LargestWindowWidth);
            int height = ReadValidValue("Enter desired height (max value {0}): ", Console.LargestWindowHeight);

            Console.SetWindowSize(width, height);
            Console.BufferWidth = Console.WindowWidth;
            Console.BufferHeight = Console.WindowHeight;

            Console.CursorVisible = false;
        }

        private static int ReadValidValue(string messageFormat, int largestWindowDimention)
        {
            Console.WriteLine(messageFormat, largestWindowDimention);
            int result;

            do
            {
                result = ReadValidIntFromUser();
                result = ProcessDimention(result);
            } while (result <= 0 || result > largestWindowDimention);

            return result;
        }

        private static int ReadValidIntFromUser()
        {
            bool parseSuccessful;
            int inputValue;

            do
            {
                string userValue = Console.ReadLine();
                parseSuccessful = Int32.TryParse(userValue, out inputValue);
            } while (!parseSuccessful || inputValue == 0);

            return inputValue;
        }

        private static int ProcessDimention(int input)
        {
            return input - (input % BLOCK_SIZE) - MIN_DIMENTION_LENGTH;
        }
    }
}
