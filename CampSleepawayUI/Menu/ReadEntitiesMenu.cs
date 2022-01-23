using CampSleepaway.Application;
using CampSleepaway.Persistence;
using System;
using System.Collections.Generic;

namespace CampSleepaway.UI.Menu
{
    class ReadEntitiesMenu : Menu
    {
        private readonly CampSleepawayContext _context = new CampSleepawayContext();

        private readonly List<string> _menuOptions = new()
        {
            "Find all Campers",
            "Find camper by Id",
            "Find all Cabins",
            "Find cabin by Id",
            "Find all Councelors",
            "Find councelor by Id",
            "Find all Next of Kins",
            "Find next of kins by Id",
            "Return to main menu"
        };

        public override Menu GetNextMenu(int input)
        {
            return input switch
            {
                1 => this,
                2 => this,
                3 => this,
                4 => this,
                5 => this,
                6 => this,
                7 => this,
                8 => this,
                9 => new MainMenu(),
                _ => null,
            };
        }

        public override int HandleInput()
        {
            int inputValue = GetIntAboveZeroFromUserInput(_menuOptions.Count);
            switch (inputValue)
            {
                case 1:
                    ShowAllCampers();
                    break;
                case 2:
                    Console.WriteLine("Enter the ID you want to find information about");
                    ShowCamperById(GetIntAboveZeroFromUserInput(int.MaxValue));
                    break;
                case 3:
                    ShowAllCabin();
                    break;
                case 4:
                    Console.WriteLine("Enter the ID you want to find information about");
                    ShowCabinById(GetIntAboveZeroFromUserInput(int.MaxValue));
                    break;
                case 5:
                    ShowAllCounselors();
                    break;
                case 6:
                    Console.WriteLine("Enter the ID you want to find information about");
                    ShowCouncelorById(GetIntAboveZeroFromUserInput(int.MaxValue));
                    break;
                case 7:
                    ShowAllNextOfKins();
                    break;
                case 8:
                    Console.WriteLine("Enter the ID you want to find information about");
                    ShowNextOfKinById(GetIntAboveZeroFromUserInput(int.MaxValue));
                    break;
                default:
                    break;
            }
            return inputValue;
        }

        private void ShowNextOfKinById(int id)
        {
            NextOfKinManager nextOfKinManager = new(_context);
            var item = nextOfKinManager.GetById(id);
            Console.WriteLine($"ID: {item.Id} First Name: {item.FirstName} Last Name: {item.LastName}");
        }

        private void ShowCouncelorById(int id)
        {
            CounselorManager counselorManager = new(_context);
            var item = counselorManager.GetById(id);
            Console.WriteLine($"ID: {item.Id} First Name: {item.FirstName} Last Name: {item.LastName}");
        }

        private void ShowCabinById(int id)
        {
            CabinManager cabinManager = new(_context);
            var item = cabinManager.GetById(id);
            Console.WriteLine($"ID: {item.Id} Name: {item.Name}");
        }

        private void ShowCamperById(int id)
        {
            CamperManager camperManager = new(_context);
            var item = camperManager.GetById(id);
            Console.WriteLine($"ID: {item.Id} First Name: {item.FirstName} Last Name: {item.LastName}");
        }

        private void ShowAllNextOfKins()
        {
            NextOfKinManager nextOfKinManager = new (_context);
            var list = nextOfKinManager.GetAllItems();
            foreach (var item in list)
            {
                Console.WriteLine($"ID: {item.Id} First Name: {item.FirstName} Last Name: {item.LastName}");
            }
        }

        private void ShowAllCounselors()
        {
            CounselorManager counselorManager = new (_context);
            var list = counselorManager.GetAllItems();
            foreach (var item in list)
            {
                Console.WriteLine($"ID: {item.Id} First Name: {item.FirstName} Last Name: {item.LastName}");
            }
        }

        private void ShowAllCabin()
        {
            CabinManager cabinManager = new(_context);
            var list = cabinManager.GetAllItems();
            foreach (var item in list)
            {
                Console.WriteLine($"ID: {item.Id} Name: {item.Name}");
            }
        }

        private void ShowAllCampers()
        {
            CamperManager camperManager = new(_context);
            var list = camperManager.GetAllItems();
            foreach (var item in list)
            {
                Console.WriteLine($"ID: {item.Id} First Name: {item.FirstName} Last Name: {item.LastName}");
            }
        }

        public override void ShowMenu()
        {
            Console.WriteLine("What would you like to find?");
            Console.WriteLine("Please select a menu option below by writing the number next to the option");
            for (int menuIndex = 0; menuIndex < _menuOptions.Count; menuIndex++)
            {
                Console.WriteLine($"{menuIndex + 1}. {_menuOptions[menuIndex]}");
            }
        }
    }
}
