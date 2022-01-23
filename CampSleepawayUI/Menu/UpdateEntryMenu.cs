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
    class UpdateEntryMenu : Menu
    {
        private readonly CampSleepawayContext _context = new CampSleepawayContext();

        private readonly List<string> _menuOptions = new()
        {
            "Change counselor responsible for a cabin",
            "Change name of Camper",
            "Return to main menu"
        };

        public override Menu GetNextMenu(int input)
        {
            return input switch
            {
                1 => this,
                2 => this,
                3 => new MainMenu(),
                _ => null,
            };
        }

        public override int HandleInput()
        {
            int inputValue = GetIntAboveZeroFromUserInput(_menuOptions.Count);
            switch (inputValue)
            {
                case 1:
                    ChangeCounselorInCabin();
                    break;
                case 2:
                    ChangeNameOfCamper();
                    break;
                default:
                    break;
            }
            return inputValue;
        }

        private void ChangeCounselorInCabin()
        {
            Console.WriteLine("For which cabin do you want to change counselor?");
            CabinManager cabinManager;
            int cabinId;
            GetCabinId(out cabinManager, out cabinId);
            int counselorId = GetCounselorToChangeId();

            Console.WriteLine("From what date?");
            DateTime startDate = GetDate();

            Console.WriteLine("To what date?");
            DateTime endDate = GetDate();
            if (HasActiveCouncelor(cabinManager, cabinId, startDate, endDate))
            {
                return;
            }
            else
            {
                cabinManager.AddCounselorToCabinById(counselorId, cabinId, startDate, endDate);
            }
        }

        private static bool HasActiveCouncelor(CabinManager cabinManager, int cabinId, DateTime startDate, DateTime endDate)
        {
            //check by start
            Counselor startCounselor = cabinManager.GetCounselorInCabinById(cabinId, startDate);
            if (startCounselor != null)
            {
                Console.WriteLine("Cabin already has a councelor at tthat start date");
                return true;
            }
            
            //check by end
            Counselor endCounselor = cabinManager.GetCounselorInCabinById(cabinId, startDate);
            if (endCounselor != null)
            {
                Console.WriteLine("Cabin already has a councelor at that end date");
                return true;
            }

            return false;
            
        }

        private int GetCounselorToChangeId()
        {
            Console.WriteLine("Which counselor should take the spot?");
            CounselorManager counselorManager = new(_context);
            List<Counselor> counselors = counselorManager.GetAllItems();
            foreach (var counselor in counselors)
            {
                Console.WriteLine($"{counselor.Id}. {counselor.Title} {counselor.FirstName} {counselor.LastName}");
            }
            Console.WriteLine("Select by writing the id");
            return GetIntAboveZeroFromUserInput(int.MaxValue);
        }

        private void GetCabinId(out CabinManager cabinManager, out int cabinId)
        {
            cabinManager = new(_context);
            List<Cabin> cabins = cabinManager.GetAllItems();
            foreach (var cabin in cabins)
            {
                Console.WriteLine($"{cabin.Id}. {cabin.Name}");
            }
            Console.WriteLine("Select by writing the id");
            cabinId = GetIntAboveZeroFromUserInput(int.MaxValue);
        }

        private static DateTime GetDate()
        {
            Console.WriteLine("Year");
            int year = GetIntAboveZeroFromUserInput(2023);
            Console.WriteLine("Month");
            int month = GetIntAboveZeroFromUserInput(12);
            Console.WriteLine("Day");
            int day = GetIntAboveZeroFromUserInput(28);
            return new DateTime(year, month, day);
        }

        private void ChangeNameOfCamper()
        {
            Console.WriteLine("Which camper do you want to update?");
            CamperManager camperManager = new(_context);
            List<Camper> campers = camperManager.GetAllItems();
            foreach (var camper in campers)
            {
                Console.WriteLine($"{camper.Id}. {camper.FirstName} {camper.LastName}");
            }
            Console.WriteLine("Select by writing the id");
            int camperIdToChange = GetIntAboveZeroFromUserInput(int.MaxValue);
            Console.WriteLine("What is the new first name?");
            string newFirstName = Console.ReadLine();
            Console.WriteLine("What is the new last name?");
            string newLastName = Console.ReadLine();

            int changes = camperManager.ChangeNameOfCamper(camperIdToChange, newFirstName, newLastName);

            Console.WriteLine($"Number of changes to the database: {changes}");
            Camper editedCamper = camperManager.GetById(camperIdToChange);
            if (editedCamper != null)
            {
                Console.WriteLine($"The result from Database: {editedCamper.FirstName} {editedCamper.LastName}");
            }
        }

        public override void ShowMenu()
        {
            Console.WriteLine("We are now ready to update entries in the database");
            Console.WriteLine("Please select a menu option below");
            for (int menuIndex = 0; menuIndex < _menuOptions.Count; menuIndex++)
            {
                Console.WriteLine($"{menuIndex + 1}. {_menuOptions[menuIndex]}");
            }
        }
    }
}
