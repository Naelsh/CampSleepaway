using CampSleepaway.Application.Cabins;
using CampSleepaway.Application.Campers;
using CampSleepaway.Application.Counselors;
using CampSleepaway.Application.NextOfKins;
using CampSleepaway.Domain.Data;
using CampSleepaway.Persistence;
using System;
using System.Linq;

namespace CampSleepaway.Application
{
    public static class SeedData
    {
        static int _amountOfCampers = 18;

        public static void CreateSeedData()
        {
            var context = new CampSleepawayContext();
            CreateCabins(context);
            CreateCampers(context);
            CreateCounselors(context);
            CreateNextOfKins(context);
            AddCounserlorsToCabins(context);
            AddCampersToCabin(context);
        }

        private static void CreateCabins(CampSleepawayContext context)
        {
            CabinManager cabinManager = new(context);
            for (int cabinIndex = 0; cabinIndex < 3; cabinIndex++)
            {
                cabinManager.AddCabinByName($"Cabin name: {cabinIndex}");
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
            }
        }

        private static void CreateNextOfKins(CampSleepawayContext context)
        {
            var camperIds = context.Campers.Select(c => new { Id = c.Id });
            for (int nokIndex = 0; nokIndex < 5; nokIndex++)
            {
                var newNextOfKin = new NextOfKin()
                {
                    FirstName = $"NextOfKin-FN{nokIndex}",
                    LastName = $"NextOfKin-LN{nokIndex}",
                    MailAddress = $"FN{nokIndex}.LN{nokIndex}@gmail.com"
                };

                newNextOfKin.Campers.Add(context.Campers.FirstOrDefault(c => c.FirstName == $"Camper-FN{nokIndex}"));

                NextOfKinManager nextOfKinManager = new(context);
                nextOfKinManager.AddNextOfKin(newNextOfKin, "Guardian");
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
                    counselorIndex + 1,
                    counselorIndex + 1,
                    startDate,
                    endDate);
            }
        }

        private static void AddCampersToCabin(CampSleepawayContext context)
        {
            CabinManager cabinManager = new CabinManager(context);
            // active
            cabinManager.AddCamperToCabin(1, 1, new DateTime(2022, 02, 01), new DateTime(2022, 04, 01));
            cabinManager.AddCamperToCabin(2, 1, new DateTime(2022, 02, 01), new DateTime(2022, 04, 01));
            cabinManager.AddCamperToCabin(3, 1, new DateTime(2022, 02, 01), new DateTime(2022, 04, 01));
            cabinManager.AddCamperToCabin(4, 1, new DateTime(2022, 02, 01), new DateTime(2022, 04, 01));
            cabinManager.AddCamperToCabin(5, 2, new DateTime(2022, 02, 01), new DateTime(2022, 04, 01));
            cabinManager.AddCamperToCabin(6, 2, new DateTime(2022, 02, 01), new DateTime(2022, 04, 01));
            cabinManager.AddCamperToCabin(7, 2, new DateTime(2022, 02, 01), new DateTime(2022, 04, 01));
            cabinManager.AddCamperToCabin(8, 2, new DateTime(2022, 02, 01), new DateTime(2022, 04, 01));
            cabinManager.AddCamperToCabin(9, 3, new DateTime(2022, 02, 01), new DateTime(2022, 04, 01));
            cabinManager.AddCamperToCabin(10, 3, new DateTime(2022, 02, 01), new DateTime(2022, 04, 01));
            cabinManager.AddCamperToCabin(11, 3, new DateTime(2022, 02, 01), new DateTime(2022, 04, 01));
            cabinManager.AddCamperToCabin(12, 3, new DateTime(2022, 02, 01), new DateTime(2022, 04, 01));
            
            // previous 
            cabinManager.AddCamperToCabin(13, 3, new DateTime(2022, 01, 02), new DateTime(2022, 01, 30));
            cabinManager.AddCamperToCabin(14, 3, new DateTime(2022, 01, 02), new DateTime(2022, 01, 30));
            cabinManager.AddCamperToCabin(15, 2, new DateTime(2022, 01, 02), new DateTime(2022, 01, 30));
            cabinManager.AddCamperToCabin(16, 2, new DateTime(2022, 01, 02), new DateTime(2022, 01, 30));
            cabinManager.AddCamperToCabin(17, 1, new DateTime(2022, 01, 02), new DateTime(2022, 01, 30));
            cabinManager.AddCamperToCabin(18, 1, new DateTime(2022, 01, 02), new DateTime(2022, 01, 30));
        }
    }
}
