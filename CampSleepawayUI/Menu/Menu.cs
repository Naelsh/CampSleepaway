using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampSleepaway.UI.Menu
{
    abstract class Menu
    {
        public abstract void ShowMenu();
        public abstract Menu GetNextMenu(int input);
        public abstract int HandleInput();

        internal static string GetStringFromUserInput(int maxLength)
        {
            string input;
            do
            {
                Console.WriteLine($"Enter a value of up to {maxLength} characters");
                input = Console.ReadLine();
            } while (input.Length > maxLength);
            return input;
        }

        internal static int GetIntAboveZeroFromUserInput(int maxNumber)
        {
            int input;
            while (!int.TryParse(Console.ReadLine(), out input) || input < 1 || input > maxNumber)
            {
                Console.WriteLine("Input not in range. Please try again");
            }
            return input;
        }
    }
}
