using CampSleepaway.UI.Menu;
using System;

namespace CampSleepawayUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu currentMenu = new MainMenu();
            while (currentMenu != null)
            {
                currentMenu.ShowMenu();
                int input = currentMenu.HandleInput();
                currentMenu = currentMenu.GetNextMenu(input);
                Console.WriteLine();
            }
            Console.WriteLine("-----");
            Console.WriteLine("Thanks for today, bye bye");
            Environment.Exit(0);
        }
    }
}
