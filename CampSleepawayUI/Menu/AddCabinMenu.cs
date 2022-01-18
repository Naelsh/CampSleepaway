using CampSleepaway.Application.Cabins;
using CampSleepaway.Domain.Data;
using CampSleepaway.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampSleepaway.UI.Menu
{
    class AddCabinMenu : Menu
    {
        private readonly List<string> _menuOptions = new()
        {
            "Add new Eagle Cabin",
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
                CabinManager manager = new(context);
                Cabin newCabin = new()
                {
                    Name = "Eagle Cabin"
                };
                manager.AddCabin(newCabin);
            }
            return inputValue;
        }

        public override void ShowMenu()
        {
            Console.WriteLine("We are now ready to add new cabin");
            Console.WriteLine("Please select a menu option below");
            for (int menuIndex = 0; menuIndex < _menuOptions.Count; menuIndex++)
            {
                Console.WriteLine($"{menuIndex + 1}. {_menuOptions[menuIndex]}");
            }
        }
    }
}
