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

            using var context = TestAddons.GetTestContext("AddNextOfKinBy");
            var nextOfKinManager = new NextOfKinManager(context);
            var nextOfKin = new NextOfKin();
            int result = nextOfKinManager.AddNextOfKin(nextOfKin);

            Assert.AreEqual(expectedEntryChangesInDB, result);
        }

        [Test]
        public void AddNextOfKinByName_FirstNameNull_NoChange()
        {
            int expectedEntryChangesInDB = 0;

            using var context = TestAddons.GetTestContext("AddNextOfKinBy");
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
            nextOfKin.Children.Add(camper);
            int result = nextOfKinManager.AddNextOfKin(nextOfKin);

            Assert.AreEqual(expectedEntryChangesInDB, result);
        }

        [Test]
        public void AddNextOfKinByName_LastNameNull_NoChange()
        {
            int expectedEntryChangesInDB = 0;

            using var context = TestAddons.GetTestContext("AddNextOfKinBy");
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
            nextOfKin.Children.Add(camper);
            int result = nextOfKinManager.AddNextOfKin(nextOfKin);

            Assert.AreEqual(expectedEntryChangesInDB, result);
        }

        [Test]
        public void AddNextOfKinByName_NoChildren_NoChange()
        {
            int expectedEntryChangesInDB = 0;

            using var context = TestAddons.GetTestContext("AddNextOfKinBy");
            var nextOfKinManager = new NextOfKinManager(context);
            var nextOfKin = new NextOfKin()
            {
                FirstName = "NextFirstName",
                LastName = "NextLastName"
            };
            int result = nextOfKinManager.AddNextOfKin(nextOfKin);

            Assert.AreEqual(expectedEntryChangesInDB, result);
        }

        [Test]
        public void CanInsertCabinIntoDatabase()
        {
            int expectedEntryChangesInDB = 3;

            using var context = TestAddons.GetTestContext("AddNextOfKinBy");

            var nextOfKinManager = new NextOfKinManager(context);
            var nextOfKin = new NextOfKin()
            { 
                FirstName = "NextFirstName",
                LastName = "NextLastName"
            };
            var camper = new Camper()
            {
                FirstName = "CamperFirstName",
                LastName = "CamperLastName",
                DateOfBirth = new DateTime(2012, 01, 01)
            };
            nextOfKin.Children.Add(camper);
            int result = nextOfKinManager.AddNextOfKin(nextOfKin);

            Assert.AreEqual(expectedEntryChangesInDB, result);
        }
    }
}
