using CampSleepaway.Application.Campers;
using CampSleepaway.Domain.Data;
using CampSleepaway.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampSleepaway.UI.Menu
{
    class AddCamperMenu : Menu
    {
        private readonly List<string> _menuOptions = new ()
        {
            "Add Camper",
            "Return to main menu"
        };


        public override Menu GetNextMenu(int input)
        {
            return input switch
            {
                1 => this,
                2 => new MainMenu(),
                _ => null,
            };
        }

        public override int HandleInput()
        {
            int inputValue = GetIntAboveZeroFromUserInput(_menuOptions.Count);
            if (inputValue == 1)
            {
                using var context = new CampSleepawayContext();
                CamperManager manager = new (context);
                Camper newCamper = new ();
                manager.AddCamper(newCamper);
            }
            return inputValue;
        }

        public override void ShowMenu()
        {
            Console.WriteLine("Welcome to the application for managing the local database Telerik");
            Console.WriteLine("What would you like to do? Enter the corresponding number");
            for (int menuIndex = 0; menuIndex < _menuOptions.Count; menuIndex++)
            {
                Console.WriteLine($"{menuIndex + 1}. {_menuOptions[menuIndex]}");
            }
        }
    }
}
