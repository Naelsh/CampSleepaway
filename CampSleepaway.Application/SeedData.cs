using CampSleepaway.Domain.Data;
using CampSleepaway.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CampSleepaway.Application
{
    public static class SeedData
    {
        static readonly int _amountOfCampers = 18;
        static readonly int _amountOfVisits = 3;
        static readonly List<int> _cabinIds = new List<int>();
        static readonly List<int> _camperIds = new List<int>();
        static readonly List<int> _counselorIds = new List<int>();
        static readonly List<int> _nextOfKinIds = new List<int>();

        public static void CreateSeedData()
        {
            var context = new CampSleepawayContext();
            CreateCabins(context);
            CreateCampers(context);
            CreateCounselors(context);
            CreateNextOfKins(context);
            AddCounserlorsToCabins(context);
            AddCampersToCabin(context);
            AddVisits(context);
        }

        private static void CreateCabins(CampSleepawayContext context)
        {
            CabinManager cabinManager = new(context);
            for (int cabinIndex = 0; cabinIndex < 3; cabinIndex++)
            {
                cabinManager.AddCabinByName($"Cabin name: {cabinIndex}");
                _cabinIds.Add(context.Cabins.First(c => c.Name == $"Cabin name: {cabinIndex}").Id);
            }
        }

        private static void CreateCounselors(CampSleepawayContext context)
        {
            CounselorManager counselorManager = new(context);
            for (int counselorIndex = 0; counselorIndex < 3; counselorIndex++)
            {
                var counselor = new Counselor()
                {
                    FirstName = $"Counselor-FN{counselorIndex}",
                    LastName = $"Counselor-LN{counselorIndex}",
                    Title = "Counselor",
                    PhoneNumber = "012345678"
                };
                counselorManager.AddCounselor(counselor);

                _counselorIds.Add(counselor.Id);
            }
        }

        private static void CreateCampers(CampSleepawayContext context)
        {
            CamperManager camperManager = new(context);
            for (int camperIndex = 0; camperIndex < _amountOfCampers; camperIndex++)
            {
                var newCamper = 
                    new Camper()
                    {
                        FirstName = $"Camper-FN{camperIndex}",
                        LastName = $"Camper-LN{camperIndex}",
                        DateOfBirth = new DateTime(2012, Math.Clamp(camperIndex, 1, 12), camperIndex + 1)
                    };
                camperManager.AddCamper(newCamper);

                _camperIds.Add(newCamper.Id);
            }
        }

        private static void CreateNextOfKins(CampSleepawayContext context)
        {
            for (int nokIndex = 0; nokIndex < 5; nokIndex++)
            {
                var newNextOfKin = new NextOfKin()
                {
                    FirstName = $"NextOfKin-FN{nokIndex}",
                    LastName = $"NextOfKin-LN{nokIndex}",
                    MailAddress = $"FN{nokIndex}.LN{nokIndex}@gmail.com"
                };

                newNextOfKin.Campers.Add(context.Campers.FirstOrDefault(c => c.Id == _camperIds[nokIndex]));

                NextOfKinManager nextOfKinManager = new(context);
                nextOfKinManager.AddNextOfKin(newNextOfKin, "Guardian");

                _nextOfKinIds.Add(newNextOfKin.Id);
            }   
        }

        private static void AddCounserlorsToCabins(CampSleepawayContext context)
        {
            CabinManager cabinManager = new (context);
            for (int counselorIndex = 0; counselorIndex < 3; counselorIndex++)
            {
                DateTime startDate = new DateTime(2022,01,01);
                DateTime endDate = new DateTime(2022,12,01);

                cabinManager.AddCounselorToCabinById(
                    _counselorIds[counselorIndex],
                    _cabinIds[counselorIndex],
                    startDate,
                    endDate);
            }
        }

        private static void AddCampersToCabin(CampSleepawayContext context)
        {
            CabinManager cabinManager = new CabinManager(context);
            // active
            cabinManager.AddCamperToCabin(_camperIds[0], _cabinIds[0], new DateTime(2022, 02, 01), new DateTime(2022, 04, 01));
            cabinManager.AddCamperToCabin(_camperIds[1], _cabinIds[0], new DateTime(2022, 02, 01), new DateTime(2022, 04, 01));
            cabinManager.AddCamperToCabin(_camperIds[2], _cabinIds[0], new DateTime(2022, 02, 01), new DateTime(2022, 04, 01));
            cabinManager.AddCamperToCabin(_camperIds[3], _cabinIds[0], new DateTime(2022, 02, 01), new DateTime(2022, 04, 01));
            cabinManager.AddCamperToCabin(_camperIds[4], _cabinIds[1], new DateTime(2022, 02, 01), new DateTime(2022, 04, 01));
            cabinManager.AddCamperToCabin(_camperIds[5], _cabinIds[1], new DateTime(2022, 02, 01), new DateTime(2022, 04, 01));
            cabinManager.AddCamperToCabin(_camperIds[6], _cabinIds[1], new DateTime(2022, 02, 01), new DateTime(2022, 04, 01));
            cabinManager.AddCamperToCabin(_camperIds[7], _cabinIds[1], new DateTime(2022, 02, 01), new DateTime(2022, 04, 01));
            cabinManager.AddCamperToCabin(_camperIds[8], _cabinIds[2], new DateTime(2022, 02, 01), new DateTime(2022, 04, 01));
            cabinManager.AddCamperToCabin(_camperIds[9], _cabinIds[2], new DateTime(2022, 02, 01), new DateTime(2022, 04, 01));
            cabinManager.AddCamperToCabin(_camperIds[10], _cabinIds[2], new DateTime(2022, 02, 01), new DateTime(2022, 04, 01));
            cabinManager.AddCamperToCabin(_camperIds[11], _cabinIds[2], new DateTime(2022, 02, 01), new DateTime(2022, 04, 01));
            
            // previous 
            cabinManager.AddCamperToCabin(_camperIds[12], _cabinIds[0], new DateTime(2022, 01, 02), new DateTime(2022, 01, 30));
            cabinManager.AddCamperToCabin(_camperIds[13], _cabinIds[0], new DateTime(2022, 01, 02), new DateTime(2022, 01, 30));
            cabinManager.AddCamperToCabin(_camperIds[14], _cabinIds[1], new DateTime(2022, 01, 02), new DateTime(2022, 01, 30));
            cabinManager.AddCamperToCabin(_camperIds[15], _cabinIds[1], new DateTime(2022, 01, 02), new DateTime(2022, 01, 30));
            cabinManager.AddCamperToCabin(_camperIds[16], _cabinIds[2], new DateTime(2022, 01, 02), new DateTime(2022, 01, 30));
            cabinManager.AddCamperToCabin(_camperIds[17], _cabinIds[2], new DateTime(2022, 01, 02), new DateTime(2022, 01, 30));
        }

        private static void AddVisits(CampSleepawayContext context)
        {
            VisitManager visitManager = new(context);

            for (int visitIndex = 0; visitIndex < _amountOfVisits; visitIndex++)
            {
                DateTime startDate = new DateTime(2022, 01, 01, 10, visitIndex, 0);
                visitManager.AddNewVisit(_camperIds[visitIndex], startDate, 60 + visitIndex, _nextOfKinIds[visitIndex]);
            }
        }
    }
}
