using CampSleepaway.Application.Cabins;
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
    class AddCamperToCabinMenu : Menu
    {
        private readonly List<string> _menuOptions = new()
        {
            "Yes",
            "No, return to main menu"
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
            PrintCampers();
            Console.WriteLine("Select by writing the ID-number");
            int inputCamperID = GetIntAboveZeroFromUserInput(int.MaxValue);
            Console.WriteLine();

            PrintCabins();
            Console.WriteLine("Select by writing the ID-number");
            int inputCabinId = GetIntAboveZeroFromUserInput(int.MaxValue);

            AddCamperToCabin(inputCamperID, inputCabinId);

            Console.WriteLine("Do you want to change another childs Cabin?");
            for (int menuIndex = 1; menuIndex <= _menuOptions.Count; menuIndex++)
            {
                Console.WriteLine($"{menuIndex}. {_menuOptions[menuIndex-1]}");
            }
            int inputValue = GetIntAboveZeroFromUserInput(_menuOptions.Count);
            return inputValue;
        }


        public override void ShowMenu()
        {
            Console.WriteLine("Which camper would you like to manage?");
            Console.WriteLine("Please select a menu option below");
        }

        private void AddCamperToCabin(int camperId, int cabinId)
        {
            using var context = new CampSleepawayContext();
            CamperManager manager = new(context);
            manager.AddCamperToCabin(camperId, cabinId);
        }

        private void PrintCampers()
        {
            using var context = new CampSleepawayContext();
            CamperManager manager = new(context);
            List<Camper> campers = manager.GetAllCampers();
            foreach (var camper in campers)
            {
                Console.WriteLine($"{camper.Id}. {camper.FirstName} {camper.LastName}");
            }
        }

        private void PrintCabins()
        {
            using var context = new CampSleepawayContext();
            CabinManager cabinManager = new(context);
            List<Cabin> cabins = cabinManager.GetAllCabins();
            foreach (var cabin in cabins)
            {
                Console.WriteLine($"{cabin.Id}. {cabin.Name}");
            }
        }
    }
}
