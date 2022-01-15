using CampSleepaway.Application.Campers;
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
            context.SaveChanges();
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
                newNextOfKin.Children.Add(context.Campers.FirstOrDefault());
                context.NextOfKins.Add(newNextOfKin);
            }
        }

        private static void CreateCabins(CampSleepawayContext context)
        {
            for (int cabinIndex = 0; cabinIndex < 3; cabinIndex++)
            {
                context.Cabins.Add(
                    new Cabin()
                    {
                        Name = $"Cabin name: {cabinIndex}"
                    });
            }
        }

        private static void CreateCounselors(CampSleepawayContext context)
        {
            for (int counselorIndex = 0; counselorIndex < 3; counselorIndex++)
            {
                var counselor = new Counselor()
                {
                    FirstName = $"Counselor-FN{counselorIndex}",
                    LastName = $"Counselor-LN{counselorIndex}",
                };
                context.Counselors.Add(counselor);
            }
        }

        private static void CreateCampers(CampSleepawayContext context)
        {
            for (int camperIndex = 0; camperIndex < 18; camperIndex++)
            {
                context.Campers.Add(
                    new Camper()
                    {
                        FirstName = $"Camper-FN{camperIndex}",
                        LastName = $"Camper-LN{camperIndex}",
                        DateOfBirth = new DateTime(2012, Math.Clamp(camperIndex, 1, 12), camperIndex + 1)
                    });
            }
        }
    }
}
