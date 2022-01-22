using CampSleepaway.Application.Cabins;
using CampSleepaway.Application.Campers;
using CampSleepaway.Domain.Data;
using CampSleepaway.Persistence;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampSleepaway.Test.Units.Application
{
    class CabinTest
    {

        [Test]
        public void AddCabinByName_EmptyString_NoChange()
        {
            int expected = 0;

            using var context = TestAddons.GetTestContext("CanInsertEmptyStringCabin");
            var cabinManager = new CabinManager(context);

            string name = null;
            int result = cabinManager.AddCabinByName(name);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void CanInsertCabinIntoDatabase()
        {
            var expected = 1;

            using var context = TestAddons.GetTestContext("CanInsertCabin");
            var cabinManager = new CabinManager(context);

            string name = "Cabin 1";
            int result = cabinManager.AddCabinByName(name);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetCabin()
        {
            string expectedName = "Cabin 1";

            using var context = TestAddons.GetTestContext("GetCabin");
            var cabinManager = new CabinManager(context);

            cabinManager.AddCabinByName("Cabin 1");

            Cabin result = cabinManager.GetCabinById(1);

            Assert.AreEqual(expectedName, result.Name);
        }

        [Test]
        public void AddCamperToCabin()
        {
            int amountOfresidents = 1;

            using var context = TestAddons.GetTestContext("AddCamper");
            var cabinManager = new CabinManager(context);
            cabinManager.AddCabinByName("Cabin");

            CamperManager camperManager = new(context);
            Camper camper = new Camper()
            {
                FirstName = "FN",
                LastName = "LN",
                DateOfBirth = new DateTime()
            };
            camperManager.AddCamper(camper);
            
            DateTime start = new DateTime(2022, 01, 22);
            DateTime end = new DateTime(2022, 02, 22);

            cabinManager.AddCamperToCabin(camper.Id, 1, start, end);

            Assert.AreEqual(amountOfresidents, context.Cabins.First().Campers.Count);
        }

        [Test]
        public void AddCamperToCabinWhichIsFull()
        {
            int amountOfresidents = 4;
            string camperNotInCabinFirstName = "NotPresent";

            using var context = TestAddons.GetTestContext("AddCamperToFullCabin");
            var cabinManager = new CabinManager(context);
            cabinManager.AddCabinByName("Cabin");

            DateTime start = new DateTime(2022, 01, 22);
            DateTime end = new DateTime(2022, 02, 22);
            CamperManager camperManager = new(context);
            for (int camperI = 0; camperI < amountOfresidents; camperI++)
            {
                Camper camper = new Camper()
                {
                    FirstName = "Present",
                    LastName = "Present",
                    DateOfBirth = new DateTime()
                };
                camperManager.AddCamper(camper);
                cabinManager.AddCamperToCabin(camper.Id, 1, start, end);
            }

            Camper newCamper = new Camper()
            {
                FirstName = "NotPresent",
                LastName = "NotPresent",
                DateOfBirth = new DateTime()
            };
            camperManager.AddCamper(newCamper);
            cabinManager.AddCamperToCabin(newCamper.Id, 1, start, end);

            Assert.AreEqual(amountOfresidents, context.Cabins.First().Campers.Count);
            Assert.AreEqual(0,
                context.Cabins.First().Campers
                .Where(c => c.FirstName == camperNotInCabinFirstName).ToList().Count);
        }

    }
}
