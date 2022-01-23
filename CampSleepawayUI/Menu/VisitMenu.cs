using CampSleepaway.Application;
using CampSleepaway.Domain.Data;
using CampSleepaway.Persistence;
using System;
using System.Collections.Generic;

namespace CampSleepaway.UI.Menu
{
    class VisitMenu : Menu
    {
        private readonly CampSleepawayContext _context = new CampSleepawayContext();

        public override Menu GetNextMenu(int input)
        {
            return input switch
            {
                0 => new MainMenu(),
                _ => this,
            };
        }

        public override int HandleInput()
        {
            return 0;
        }

        public override void ShowMenu()
        {
            Console.WriteLine("So you want to visit a camper?");
            Console.WriteLine("Please select a camper from the options below");
            ListCampers();
            int camperId = GetIntAboveZeroFromUserInput(int.MaxValue);

            Console.WriteLine("");
            Console.WriteLine("For which next of kin is this?");
            ListNextOfKins();
            int nextOfKinId = GetIntAboveZeroFromUserInput(int.MaxValue);

            ShowVisitIfValid(camperId, nextOfKinId);
        }

        private void ShowVisitIfValid(int camperId, int nextOfKinId)
        {
            VisitManager visitManager = new(_context);
            List<VisitView> visits = visitManager.GetVisits(camperId, nextOfKinId);
            if (visits.Count == 0)
            {
                Console.WriteLine("There are no visits for this Next of kin and that camper in the database");
            }
            else
            {
                foreach (VisitView visit in visits)
                {
                    string startTime = visit.StartTime.ToString("HH:mm:ss");
                    string endTime = visit.EndTime.ToString("HH:mm:ss");
                    Console.WriteLine($"{visit.CamperName} is in cabin {visit.CabinName}");
                    Console.WriteLine($"Counselor --{visit.CounselorTitle}-- {visit.CounselorName} Tel: {visit.CounselorPhoneNumber}");
                    Console.WriteLine($"The visit is at {startTime} - {endTime}");
                }
            }
        }

        private void ListNextOfKins()
        {
            NextOfKinManager nextOfKinManager = new(_context);
            List<NextOfKin> nextOfKins = nextOfKinManager.GetAllItems();
            foreach (NextOfKin nextOfKin in nextOfKins)
            {
                Console.WriteLine($"{nextOfKin.Id}. {nextOfKin.FirstName} {nextOfKin.LastName}");
            }
        }

        private void ListCampers()
        {
            CamperManager camperManager = new(_context);
            List<Camper> campers = camperManager.GetAllItems();
            foreach (Camper camper in campers)
            {
                Console.WriteLine($"{camper.Id}. {camper.FirstName} {camper.LastName}");
            }
        }
    }
}
