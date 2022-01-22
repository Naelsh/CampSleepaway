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
            "Remove all data in database",
            "Seed Data",
            "Add Entry",
            "Read Entities",
            "Add Relations", // TODO
            "Delete entity", // TODO
            "Reports", // check if done
            "Exit Application"
        };

        public override Menu GetNextMenu(int input)
        {
            return input switch
            {
                1 => this,
                2 => this,
                3 => new AddEntry(),
                4 => new ReadEntitiesMenu(),
                5 => new AddRelationsMenu(),
                6 => this, //DeleteEntryMenu(),
                7 => new CamperReportMenu(),
                8 => null,
                _ => null,
            };
        }

        public override int HandleInput()
        {
            int inputValue = GetIntAboveZeroFromUserInput(_menuOptions.Count);
            if (inputValue == 2)
            {
                SeedData.CreateSeedData();
            }
            else if (inputValue == 1)
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
