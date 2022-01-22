using CampSleepaway.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampSleepaway.UI.Menu
{
    class MainMenu : Menu
    {
        private readonly List<string> _menuOptions = new ()
        {
            "Seed Data",
            "Add Camper",
            "Read Entities",
            "Camper Reports",
            //"Add Cabin",
            //"Add Counselor",
            //"Add Next of Kin",
            "Remove all data in database",
            "Exit Application"
        };

        public override Menu GetNextMenu(int input)
        {
            return input switch
            {
                1 => this,
                2 => new AddCamperMenu(),
                3 => new ReadEntitiesMenu(),
                4 => new CamperReportMenu(),
                5 => this,
                6 => null,
                _ => null,
            };
        }

        public override int HandleInput()
        {
            int inputValue = GetIntAboveZeroFromUserInput(_menuOptions.Count);
            if (inputValue == 1)
            {
                SeedData.CreateSeedData();
            }
            if (inputValue == 5)
            {
                ClearDatabase.Clear();
            }
            return inputValue;
        }

        public override void ShowMenu()
        {
            Console.WriteLine("Welcome to the application for managing the Camp Sleepaway");
            Console.WriteLine("What would you like to do? Enter the corresponding number");
            for (int menuIndex = 0; menuIndex < _menuOptions.Count; menuIndex++)
            {
                Console.WriteLine($"{menuIndex + 1}. {_menuOptions[menuIndex]}");
            }
        }
    }
}
