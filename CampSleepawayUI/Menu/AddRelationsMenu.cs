using CampSleepaway.Application;
using CampSleepaway.Domain.Data;
using CampSleepaway.Persistence;
using System;
using System.Collections.Generic;

namespace CampSleepaway.UI.Menu
{
    class AddRelationsMenu : Menu
    {
        private readonly CampSleepawayContext _context = new CampSleepawayContext();

        private readonly List<string> _menuOptions = new()
        {
            "Connect Next of Kin to Camper",
            "Add Camper to Cabin",
            "Add Counselor to Cabin",
            "Add new Visit",
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
                5 => new MainMenu(),
                _ => null,
            };
        }

        public override int HandleInput()
        {
            int inputValue = GetIntAboveZeroFromUserInput(_menuOptions.Count);
            switch (inputValue)
            {
                case 1:
                    ConnectNextOfKinToCamper();
                    break;
                case 2:
                    AddCamperToCabin();
                    break;
                case 3:
                    AddCounselorToCabin();
                    break;
                case 4:
                    AddVisit();
                    break;
                default:
                    break;
            }
            return inputValue;
        }

        private void AddVisit()
        {
            AskForSelection();
            Console.WriteLine("Select Camper");
            ListCampers();
            int camperId = GetIntAboveZeroFromUserInput(int.MaxValue);
            Console.WriteLine("");
            Console.WriteLine("Select Next of Kin, remember that the next of Kin needs to have a relation to the camper");
            ListNextOfKins();
            int nextOfKinId = GetIntAboveZeroFromUserInput(int.MaxValue);
            Console.WriteLine("");
            Console.WriteLine("What time do you want to visit? Visiting hours 10-20");
            Console.WriteLine("Year:");
            int year = GetIntAboveZeroFromUserInput(2023);
            Console.WriteLine("Month:");
            int month = GetIntAboveZeroFromUserInput(12);
            Console.WriteLine("Day:");
            int day = GetIntAboveZeroFromUserInput(28);
            Console.WriteLine("Hour:");
            int hour = GetIntAboveZeroFromUserInput(24);
            Console.WriteLine("Minute:");
            int minute = GetIntAboveZeroFromUserInput(60);
            DateTime startTime = new (year, month, day, hour, minute, 0);
            Console.WriteLine("");
            Console.WriteLine("For how long? (Minutes)");
            int durationInMinutes = GetIntAboveZeroFromUserInput(60 * 10);

            AddNewVisit(camperId, startTime, durationInMinutes, nextOfKinId);
        }

        private void AddNewVisit(int camperId, DateTime startTime, int durationInMinutes, int nextOfKinId)
        {
            VisitManager visitManager = new(_context);
            var result = visitManager.AddNewVisit(camperId, startTime, durationInMinutes, nextOfKinId);
            Console.WriteLine($"A total of {result} entries were added to the database");
        }

        private void AddCounselorToCabin()
        {
            AskForSelection();
            Console.WriteLine("Select Counselor");
            ListCounselors();
            int counselorToCabinId = GetIntAboveZeroFromUserInput(int.MaxValue);
            Console.WriteLine("Select Cabin");
            ListCabins();
            int counselorsCabinId = GetIntAboveZeroFromUserInput(int.MaxValue);
            Console.WriteLine("From what date is the cunselor supposed to be responsible?");
            Console.WriteLine("Year:");
            int counselorYear = GetIntAboveZeroFromUserInput(2023);
            Console.WriteLine("Month:");
            int counselorMonth = GetIntAboveZeroFromUserInput(12);
            Console.WriteLine("Day:");
            int counselorDay = GetIntAboveZeroFromUserInput(28);
            Console.WriteLine("For how many days?");
            int counselorNumDays = GetIntAboveZeroFromUserInput(int.MaxValue);
            DateTime counselorStartDate = new DateTime(counselorYear, counselorMonth, counselorDay);
            DateTime counselorEndDate = counselorStartDate.AddDays(counselorNumDays);
            AddCounselorToCabin(counselorToCabinId, counselorsCabinId, counselorStartDate, counselorEndDate);
        }

        private void AddCamperToCabin()
        {
            AskForSelection();
            Console.WriteLine("Select Camper");
            ListCampers();
            int camperToCabinId = GetIntAboveZeroFromUserInput(int.MaxValue);
            Console.WriteLine("Select Cabin");
            ListCabins();
            int cabinId = GetIntAboveZeroFromUserInput(int.MaxValue);
            Console.WriteLine("From what date is the camper supposed to stay?");
            Console.WriteLine("Year:");
            int year = GetIntAboveZeroFromUserInput(2023);
            Console.WriteLine("Month:");
            int month = GetIntAboveZeroFromUserInput(12);
            Console.WriteLine("Day:");
            int day = GetIntAboveZeroFromUserInput(28);
            Console.WriteLine("For how many days?");
            int numDays = GetIntAboveZeroFromUserInput(int.MaxValue);
            DateTime startDate = new DateTime(year, month, day);
            DateTime endDate = startDate.AddDays(numDays);
            AddCamperToCabin(camperToCabinId, cabinId, startDate, endDate);
        }

        private void ConnectNextOfKinToCamper()
        {
            AskForSelection();
            Console.WriteLine("Select Next of kin");
            ListNextOfKins();
            int nextOfKinId = GetIntAboveZeroFromUserInput(int.MaxValue);
            Console.WriteLine("Select Camper");
            ListCampers();
            int camperId = GetIntAboveZeroFromUserInput(int.MaxValue);
            Console.WriteLine("What is the next of kins relationship to the Camper?");
            string relationship = Console.ReadLine();
            AddNextOfKinToCamperRelationship(nextOfKinId, camperId, relationship);
        }

        private void AddCounselorToCabin(int counselorId, int cabinId, DateTime start, DateTime end)
        {
            CabinManager cabinManager = new(_context);
            int result = cabinManager.AddCounselorToCabinById(counselorId, cabinId, start, end);
            Console.WriteLine($"A total of {result} entries were added");
        }

        private void AddCamperToCabin(int camperId, int cabinId, DateTime start, DateTime end)
        {
            CabinManager cabinManager = new(_context);
            int result = cabinManager.AddCamperToCabin(camperId, cabinId, start, end);
            Console.WriteLine($"A total of {result} entries were added");
        }

        private void AddNextOfKinToCamperRelationship(int nextOfKinId, int camperId, string relationship)
        {
            NextOfKinManager nextOfKinManager = new(_context);
            int result = nextOfKinManager.AddNextOfKinRelationship(nextOfKinId, camperId, relationship);
            Console.WriteLine($"A total of {result} entries were added");
        }

        private void ListCounselors()
        {
            CounselorManager counselorManager = new(_context);
            List<Counselor> counselors = counselorManager.GetAllItems();
            foreach (var counselor in counselors)
            {
                Console.WriteLine($"{counselor.Id}. {counselor.FirstName} {counselor.LastName}");
            }
        }

        private void ListCabins()
        {
            CabinManager cabinManager = new(_context);
            List<Cabin> cabins = cabinManager.GetAllItems();
            foreach (var cabin in cabins)
            {
                Console.WriteLine($"{cabin.Id}. {cabin.Name}");
            }
        }


        private void ListCampers()
        {
            CamperManager camperManager = new(_context);
            List<Camper> campers = camperManager.GetAllItems();
            foreach (var camper in campers)
            {
                Console.WriteLine($"{camper.Id}. {camper.FirstName} {camper.LastName}");
            }
        }

        private void ListNextOfKins()
        {
            NextOfKinManager nextOfKinManager = new(_context);
            List<NextOfKin> nextOfKins = nextOfKinManager.GetAllItems();
            foreach (var nextOfKin in nextOfKins)
            {
                Console.WriteLine($"{nextOfKin.Id}. {nextOfKin.FirstName} {nextOfKin.LastName}");
            }
        }

        private void AskForSelection()
        {
            Console.WriteLine("Please select one of the following entries by entering the ID of the entity");
            Console.WriteLine("Note that if you pick a number that does not exist unfortunate consequences might occur");
        }

        public override void ShowMenu()
        {
            Console.WriteLine("We are now ready to add new relationships between entries in the database");
            Console.WriteLine("Please select a menu option below");
            for (int menuIndex = 0; menuIndex < _menuOptions.Count; menuIndex++)
            {
                Console.WriteLine($"{menuIndex + 1}. {_menuOptions[menuIndex]}");
            }
        }
    }
}
