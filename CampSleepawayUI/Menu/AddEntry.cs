using CampSleepaway.Application;
using CampSleepaway.Domain.Data;
using CampSleepaway.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CampSleepaway.UI.Menu
{
    class AddEntry : Menu
    {
        private readonly CampSleepawayContext _context = new CampSleepawayContext();

        private readonly List<string> _menuOptions = new ()
        {
            "Add new Camper with dummy data",
            "Add custom Camper",
            "Add new Next of kin with dummy data",
            "Add custom Next of kin",
            "Add new Cabin with dummy data",
            "Add custom Cabin",
            "Add new Counselor with dummy data",
            "Add custom Counselor",
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
                    AddCamper("10y FN", "10y LN", DateTime.Now.AddYears(-10));
                    break;
                case 2:
                    AddCustomCamper();
                    break;
                case 3:
                    AddNextOfKin("NewNext FN", "NexNext LN", "test@test.test", _context.Campers.FirstOrDefault().Id, "Guardian");
                    break;
                case 4:
                    AddCustomNextOfKin();
                    break;
                case 5:
                    AddCabin("New Cabin");
                    break;
                case 6:
                    AddCustomCabin();
                    break;
                case 7:
                    AddCounselor("Counselor FN", "Counselor LN", "Cabin King");
                    break;
                case 8:
                    AddCustomCounselor();
                    break;
                default:
                    break;
            }
            return inputValue;
        }

        private void AddCustomCounselor()
        {
            Console.WriteLine("Enter first name");
            string counselorFirstName = Console.ReadLine();
            Console.WriteLine("Enter last name");
            string counselorLastName = Console.ReadLine();
            Console.WriteLine("Enter the title for the counselor");
            string title = Console.ReadLine();
            AddCounselor(counselorFirstName, counselorLastName, title);
        }

        private void AddCustomCabin()
        {
            Console.WriteLine("What is the name of the Cabin?");
            string cabinName = Console.ReadLine();
            AddCabin(cabinName);
        }

        private void AddCustomNextOfKin()
        {
            Console.WriteLine("Enter first name");
            string nextOfKinFirstName = Console.ReadLine();
            Console.WriteLine("Enter last name");
            string nextOfKinLastName = Console.ReadLine();
            Console.WriteLine("Mail address");
            string mailAddress = Console.ReadLine();
            Console.WriteLine("Enter ID of one camper the next of kin is responsible for");
            int camperId = GetIntAboveZeroFromUserInput(int.MaxValue);
            Console.WriteLine("What relationship does the next of kin have to the camper?");
            string relationship = Console.ReadLine();
            AddNextOfKin(nextOfKinFirstName, nextOfKinLastName, mailAddress, camperId, relationship);
        }

        private void AddCustomCamper()
        {
            Console.WriteLine("Enter first name");
            string firstName = Console.ReadLine();
            Console.WriteLine("Enter last name");
            string lastName = Console.ReadLine();
            Console.WriteLine("What year is the camper born?");
            int year = GetIntAboveZeroFromUserInput(2022);
            Console.WriteLine("What month is the camper born?");
            int month = GetIntAboveZeroFromUserInput(12);
            Console.WriteLine("What day is the camper born?");
            int day = GetIntAboveZeroFromUserInput(28);
            AddCamper(firstName, lastName, new DateTime(year, month, day));
        }

        private void AddCounselor(string firstName, string lastName, string title)
        {
            Counselor newCounselor = new()
            {
                FirstName = firstName,
                LastName = lastName,
                Title = title
            };
            CounselorManager counselorManager = new(_context);
            counselorManager.AddCounselor(newCounselor);
        }

        private void AddCabin(string name)
        {
            CabinManager cabinManager = new(_context);
            cabinManager.AddCabinByName(name);
        }

        private void AddNextOfKin(string firstName, string lastName, string mailAddress, int camperId, string relationship)
        {
            NextOfKin newNextOfKin = new()
            {
                FirstName = firstName,
                LastName = lastName,
                MailAddress = mailAddress
            };
            CamperManager camperManager = new (_context);
            Camper camper = camperManager.GetById(camperId);
            newNextOfKin.Campers.Add(camper);

            NextOfKinManager nextOfKinManager = new(_context);
            nextOfKinManager.AddNextOfKin(newNextOfKin, relationship);
        }

        private void AddCamper(string firstName, string lastName, DateTime dateOfBirth)
        {
            CamperManager manager = new(_context);
            Camper newCamper = new()
            {
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dateOfBirth
            };
            manager.AddCamper(newCamper);
        }

        public override void ShowMenu()
        {
            Console.WriteLine("We are now ready to add new entries to the database");
            Console.WriteLine("Please select a menu option below");
            for (int menuIndex = 0; menuIndex < _menuOptions.Count; menuIndex++)
            {
                Console.WriteLine($"{menuIndex + 1}. {_menuOptions[menuIndex]}");
            }
        }
    }
}
