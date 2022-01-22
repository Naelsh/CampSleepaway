using CampSleepaway.Application.Cabins;
using CampSleepaway.Application.Campers;
using CampSleepaway.Application.Counselors;
using CampSleepaway.Application.NextOfKins;
using CampSleepaway.Domain.Data;
using CampSleepaway.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampSleepaway.Application
{
    public static class SeedData
    {
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
            for (int camperIndex = 0; camperIndex < 18; camperIndex++)
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

                newNextOfKin.Camper.Add(context.Campers.FirstOrDefault(c => c.Id == nokIndex + 1));

                NextOfKinManager nextOfKinManager = new(context);
                nextOfKinManager.AddNextOfKin(newNextOfKin, "Guardian");
            }   
        }

        private static void AddCounserlorsToCabins(CampSleepawayContext context)
        {
            CabinManager cabinManager = new (context);
            for (int counserlorIndex = 0; counserlorIndex < 3; counserlorIndex++)
            {
                DateTime startDate = new DateTime(2022,01,01);
                DateTime endDate = new DateTime(2022,12,01);

                cabinManager.AddCounselorToCabinById(
                    counserlorIndex + 1,
                    counserlorIndex + 1,
                    startDate,
                    endDate);
            }
        }

        private static void SetRandomStartAndEndDate(out DateTime startDate, out DateTime endDate)
        {
            Random random = new Random();
            int startYear = random.Next(2021, 2022);
            int startMonth = random.Next(1, 12);
            int startDay = random.Next(1, 28);

            int endYear = random.Next(2021, 2022);
            int endMonth = random.Next(1, 12);
            int endDay = random.Next(1, 28);

            startDate = new DateTime(startYear, startMonth, startDay);
            endDate = new DateTime(endYear, endMonth, endDay);

            if (startDate.CompareTo(endDate) > 0)
            {
                DateTime temp = startDate;
                startDate = endDate;
                endDate = temp;
            }
            else if (startDate.CompareTo(endDate) == 0)
            {
                endDate.AddDays(1);
            }
        }

        private static void AddCampersToCabin(CampSleepawayContext context)
        {
            throw new NotImplementedException();
        }
    }
}
