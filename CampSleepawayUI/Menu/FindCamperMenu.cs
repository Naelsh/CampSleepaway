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
    class FindCamperMenu : Menu
    {
        private readonly List<string> _menuOptions = new()
        {
            "Find all campers",
            "Find camper by name",
            "Find camper by id",
            "Return to main menu"
        };

        public override Menu GetNextMenu(int input)
        {
            return input switch
            {
                1 => this,
                2 => this,
                3 => this,
                4 => new MainMenu(),
                _ => null,
            };
        }

        public override int HandleInput()
        {
            int inputValue = GetIntAboveZeroFromUserInput(_menuOptions.Count);
            switch (inputValue)
            {
                case 1:
                    ListAllCampers();
                    break;
                case 2:
                    GetCampersByName();
                    break;
                case 3:
                    GetCamperById();
                    break;
                default:
                    break;
            }
            return inputValue;
        }

        private void GetCamperById()
        {
            Console.WriteLine("Enter the id of the person you whish to search for");
            int id = GetIntAboveZeroFromUserInput(int.MaxValue);
            using var context = new CampSleepawayContext();
            CamperManager manager = new(context);
            Camper camper = manager.GetCamperById(id);
            if (camper == null)
            {
                Console.WriteLine("Camper not found");
            }
            else
            {
                PrintCamper(camper);
            }
        }

        private void GetCampersByName()
        {
            Console.WriteLine("Enter the first name of the person you whish to search for");
            string firstName = Console.ReadLine();
            using var context = new CampSleepawayContext();
            CamperManager manager = new(context);
            List<Camper> campers = manager.GetCampersByName(firstName);
            if (campers.Count == 0)
            {
                Console.WriteLine("No campers found with that name");
            }
            else
            {
                campers.OrderBy(x => x.Id);
                foreach (Camper camper in campers)
                {
                    PrintCamper(camper);
                }
            }
        }

        private void ListAllCampers()
        {
            using var context = new CampSleepawayContext();
            CamperManager manager = new(context);
            List<Camper> campers = manager.GetAllCampers();
            campers.OrderBy(x => x.Id);

            foreach (Camper camper in campers)
            {
                PrintCamper(camper);
            }
        }

        private static void PrintCamper(Camper camper)
        {
            Console.WriteLine($"{camper.Id} - NAME: {camper.FirstName} {camper.LastName}");
            Console.WriteLine($"BIRTHDAY: {camper.DateOfBirth.Year}/{camper.DateOfBirth.Month}/{camper.DateOfBirth.Day}");
            Console.WriteLine();
        }

        public override void ShowMenu()
        {
            Console.WriteLine("We are now ready to add new campers");
            Console.WriteLine("Please select a menu option below");
            for (int menuIndex = 0; menuIndex < _menuOptions.Count; menuIndex++)
            {
                Console.WriteLine($"{menuIndex + 1}. {_menuOptions[menuIndex]}");
            }
        }
    }
}
