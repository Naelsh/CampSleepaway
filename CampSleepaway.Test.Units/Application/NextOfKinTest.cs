using CampSleepaway.Application.Campers;
using CampSleepaway.Application.NextOfKins;
using CampSleepaway.Domain.Data;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampSleepaway.Test.Units.Application
{
    class NextOfKinTest
    {
        [Test]
        public void AddNextOfKinByName_AllPropsNull_NoChange()
        {
            int expectedEntryChangesInDB = 0;

            using var context = TestAddons.GetTestContext("AddNextOfKinByName_AllPropsNull_NoChange");
            var nextOfKinManager = new NextOfKinManager(context);
            var nextOfKin = new NextOfKin();
            int result = nextOfKinManager.AddNextOfKin(nextOfKin, "parent");

            Assert.AreEqual(expectedEntryChangesInDB, result);
        }

        [Test]
        public void AddNextOfKinByName_FirstNameNull_NoChange()
        {
            int expectedEntryChangesInDB = 0;

            using var context = TestAddons.GetTestContext("AddNextOfKinByName_FirstNameNull_NoChange");
            var nextOfKinManager = new NextOfKinManager(context);
            var nextOfKin = new NextOfKin()
            {
                LastName = "NextLastName"
            };
            var camper = new Camper()
            {
                FirstName = "CamperFirstName",
                LastName = "CamperLastName",
                DateOfBirth = new DateTime(2012, 01, 01)
            };
            nextOfKin.Campers.Add(camper);
            int result = nextOfKinManager.AddNextOfKin(nextOfKin, "parent");

            Assert.AreEqual(expectedEntryChangesInDB, result);
        }

        [Test]
        public void AddNextOfKinByName_LastNameNull_NoChange()
        {
            int expectedEntryChangesInDB = 0;

            using var context = TestAddons.GetTestContext("AddNextOfKinByName_LastNameNull_NoChange");
            var nextOfKinManager = new NextOfKinManager(context);
            var nextOfKin = new NextOfKin()
            {
                FirstName = "NextFirstName"
            };
            var camper = new Camper()
            {
                FirstName = "CamperFirstName",
                LastName = "CamperLastName",
                DateOfBirth = new DateTime(2012, 01, 01)
            };
            nextOfKin.Campers.Add(camper);
            int result = nextOfKinManager.AddNextOfKin(nextOfKin, "parent");

            Assert.AreEqual(expectedEntryChangesInDB, result);
        }

        [Test]
        public void AddNextOfKinByName_NoChildren_NoChange()
        {
            int expectedEntryChangesInDB = 0;

            using var context = TestAddons.GetTestContext("AddNextOfKinByName_NoChildren_NoChange");
            var nextOfKinManager = new NextOfKinManager(context);
            var nextOfKin = new NextOfKin()
            {
                FirstName = "NextFirstName",
                LastName = "NextLastName"
            };
            int result = nextOfKinManager.AddNextOfKin(nextOfKin, "parent");

            Assert.AreEqual(expectedEntryChangesInDB, result);
        }

        [Test]
        public void CanInsertCabinIntoDatabase()
        {
            int expectedEntryChangesInDB = 2;

            using var context = TestAddons.GetTestContext("CanInsertCabinIntoDatabase");

            var camper = new Camper()
            {
                FirstName = "CamperFirstName",
                LastName = "CamperLastName",
                DateOfBirth = new DateTime(2012, 01, 01)
            };
            CamperManager camperManager = new(context);
            camperManager.AddCamper(camper);

            var nextOfKinManager = new NextOfKinManager(context);
            var nextOfKin = new NextOfKin()
            { 
                FirstName = "NextFirstName",
                LastName = "NextLastName"
            };
            nextOfKin.Campers.Add(camper);
            int result = nextOfKinManager.AddNextOfKin(nextOfKin, "parent");

            Assert.AreEqual(expectedEntryChangesInDB, result);
        }

        [Test]
        public void AddNewChildRelationship()
        {
            string expectedNextOfKinName = "NextFirstName";
            int expectedRelationships = 1;
            string expectedRelationship = "Best Parent";

            using var context = TestAddons.GetTestContext("AddNewChildRelationship");

            var camper = new Camper()
            {
                FirstName = "CamperFirstName",
                LastName = "CamperLastName",
                DateOfBirth = new DateTime(2012, 01, 01)
            };
            CamperManager camperManager = new(context);
            camperManager.AddCamper(camper);

            var nextOfKinManager = new NextOfKinManager(context);
            var nextOfKin = new NextOfKin()
            {
                FirstName = "NextFirstName",
                LastName = "NextLastName"
            };
            nextOfKin.Campers.Add(camper);
            nextOfKinManager.AddNextOfKin(nextOfKin, "parent");

            var newCamper = new Camper()
            {
                FirstName = "Favorite Child",
                LastName = "I Promese",
                DateOfBirth = new DateTime(2011, 11, 11)
            };
            camperManager.AddCamper(newCamper);

            nextOfKinManager.AddNextOfKinRelationship(nextOfKin.Id, newCamper.Id, "Best Parent");

            Assert.AreEqual(expectedNextOfKinName,
                context.Campers.FirstOrDefault(c => c.Id == 2)
                .NextOfKins.FirstOrDefault().FirstName);
            Assert.AreEqual(expectedRelationships,
                context.CamperNextOfKins.Where(cnok => cnok.CamperId == newCamper.Id).Count());
            Assert.AreEqual(expectedRelationship,
                context.CamperNextOfKins.FirstOrDefault(cnok => cnok.CamperId == newCamper.Id).NextOfKinRelationship);
        }
    }
}
