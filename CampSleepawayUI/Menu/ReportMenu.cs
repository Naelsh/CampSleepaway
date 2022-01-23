using CampSleepaway.Application.Cabins;
using CampSleepaway.Application.Campers;
using CampSleepaway.Application.Counselors;
using CampSleepaway.Domain.Data;
using CampSleepaway.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampSleepaway.UI.Menu
{
    class ReportMenu : Menu
    {
        private readonly CampSleepawayContext _context = new CampSleepawayContext();

        private readonly List<string> _menuOptions = new()
        {
            "Find all campers living in a Cabin",
            "Find all campers by Counselor",
            "Get campers and next of kins orderd by cabin",
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
                    ListCampersInCabinById();
                    break;
                case 2:
                    ListCampersBasedOnCurrentCounselor();
                    break;
                case 3:
                    GetCampersAndNextOfKinOrderdByCabin();
                    break;
                default:
                    break;
            }
            return inputValue;
        }

        private void GetCampersAndNextOfKinOrderdByCabin()
        {
            Console.WriteLine("At what date are you interested in finding information?");
            Console.WriteLine("Year");
            int year = GetIntAboveZeroFromUserInput(2023);
            Console.WriteLine("Month");
            int month = GetIntAboveZeroFromUserInput(12);
            Console.WriteLine("Day");
            int day = GetIntAboveZeroFromUserInput(28);
            DateTime date = new DateTime(year, month, day);

            CamperManager camperManager = new(_context);
            List<CamperCabinView> campers = camperManager.GetAllCamperAndNextOfKinRelationsOrderedByCabin(date);
            campers.OrderBy(cc => cc.CabinId);
            foreach (var camper in campers)
            {
                Console.WriteLine($"Cabin: {camper.CabinId}. {camper.CabinName}");// {row.CabinName}");
                Console.WriteLine($"Camper: {camper.CamperId}. {camper.CamperFirstName} {camper.CamperLastName}");
                foreach (var nextOfKin in camper.NextOfKins)
                {
                    Console.WriteLine($"  Next Of Kin: {nextOfKin.NextOfKinId}. {nextOfKin.Relationship} {nextOfKin.FirstName} {nextOfKin.LastName}");
                }
                Console.WriteLine("------------------------------------");
            }
        }

        private void ListCampersBasedOnCurrentCounselor()
        {
            Console.WriteLine("Enter the id of the counselor you whish to find campers living with");
            int counselorId = GetIntAboveZeroFromUserInput(int.MaxValue);
            Console.WriteLine("At what date?");
            Console.WriteLine("Year");
            int year = GetIntAboveZeroFromUserInput(2023);
            Console.WriteLine("Month");
            int month = GetIntAboveZeroFromUserInput(12);
            Console.WriteLine("Day");
            int day = GetIntAboveZeroFromUserInput(28);
            DateTime date = new DateTime(year, month, day);

            CounselorManager counselorManager = new(_context);
            Cabin cabin = counselorManager.GetActiveCabin(counselorId, date);
            if (cabin == null)
            {
                Console.WriteLine("The counselor was not managing a cabin at that time");
            }
            else
            {
                CabinManager cabinManager = new(_context);
                List<Camper> campers = cabinManager.GetActiveCampersInCabinById(cabin.Id, date).ToList();
                if (campers.Count == 0)
                {
                    Console.WriteLine("There are no campers in this cabin at the asked time");
                }
                else
                {
                    foreach (var camper in campers)
                    {
                        PrintCamper(camper);
                    }
                }
            }
        }

        private void ListCampersInCabinById()
        {
            Console.WriteLine("Enter the id of the cabin you whish to find campers living in");
            int cabinId = GetIntAboveZeroFromUserInput(int.MaxValue);
            Console.WriteLine("At what date?");
            Console.WriteLine("Year");
            int year = GetIntAboveZeroFromUserInput(2023);
            Console.WriteLine("Month");
            int month = GetIntAboveZeroFromUserInput(12);
            Console.WriteLine("Day");
            int day = GetIntAboveZeroFromUserInput(28);
            DateTime date = new DateTime(year, month, day);

            CabinManager cabinManager = new(_context);
            List<Camper> campers = cabinManager.GetActiveCampersInCabinById(cabinId, date).ToList();
            Counselor counselor = cabinManager.GetCounselorInCabinById(cabinId, date);

            if (counselor == null)
                Console.WriteLine("WARNING! There is no councelor in the cabin");
            else
                Console.WriteLine($"Counselor: ID {counselor.Id}. Name: {counselor.FirstName} {counselor.LastName}");

            if (campers.Count == 0)
            {
                Console.WriteLine("There are no campers in this cabin at the asked time");
            }
            else
            {
                foreach (var camper in campers)
                {
                    PrintCamper(camper);
                }
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
