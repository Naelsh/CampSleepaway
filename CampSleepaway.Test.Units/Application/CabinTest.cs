using CampSleepaway.Application.Cabins;
using CampSleepaway.Application.Campers;
using CampSleepaway.Application.Counselors;
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
        public void AddCounselorToCabin()
        {
            int expectedAmountOfConselors = 1;
            using var context = TestAddons.GetTestContext("CounselorToCabin");
            var cabinManager = new CabinManager(context);
            cabinManager.AddCabinByName("Cabin");

            Counselor counselor = new Counselor()
            {
                FirstName = "Councelor",
                LastName = "Counselor",
                Title = "Lord",
                PhoneNumber = "010-123456"
            };
            CounselorManager counselorManager = new(context);
            counselorManager.AddCounselor(counselor);

            cabinManager.AddCounselorToCabinById(counselor.Id, 1,
                new DateTime(2022,01,01), new DateTime(2022, 12, 01));

            Assert.AreEqual(expectedAmountOfConselors,
                context.CabinCounselorStays.Where(ccs => ccs.CounselorId == counselor.Id).Count());

        }

        [Test]
        public void AddCounselorToCabin_CounselorAlreadyResponsibleForCabin()
        {
            int expectedAmountOfConselors = 1;
            using var context = TestAddons.GetTestContext("CounselorResponsibleAnotherToCabin");
            var cabinManager = new CabinManager(context);
            cabinManager.AddCabinByName("Cabin");

            Counselor counselor = new Counselor()
            {
                FirstName = "Councelor",
                LastName = "Counselor",
                Title = "Lord",
                PhoneNumber = "010-123456"
            };
            CounselorManager counselorManager = new(context);
            counselorManager.AddCounselor(counselor);

            cabinManager.AddCounselorToCabinById(counselor.Id, 1,
                new DateTime(2022, 01, 01), new DateTime(2022, 12, 01));

            cabinManager.AddCabinByName("SecondCabin");
            cabinManager.AddCounselorToCabinById(counselor.Id, 2,
                new DateTime(2022, 01, 02), new DateTime(2022, 11, 01));

            Assert.AreEqual(expectedAmountOfConselors,
                context.CabinCounselorStays.Where(ccs => ccs.CounselorId == counselor.Id).Count());
        }

        [Test]
        public void AddCounselorToCabinWhichHasCouncelor()
        {
            int expectedAmountOfCounselor2 = 0;
            using var context = TestAddons.GetTestContext("CounselorResponsibleToOccupiedCabin");
            var cabinManager = new CabinManager(context);
            cabinManager.AddCabinByName("Cabin");

            Counselor counselor = new Counselor()
            {
                FirstName = "Councelor",
                LastName = "Counselor",
                Title = "Lord",
                PhoneNumber = "010-123456"
            };
            Counselor counselor2 = new Counselor()
            {
                FirstName = "Councelor2",
                LastName = "Counselor2",
                Title = "Lord",
                PhoneNumber = "010-123456"
            };
            CounselorManager counselorManager = new(context);
            counselorManager.AddCounselor(counselor);
            counselorManager.AddCounselor(counselor2);

            cabinManager.AddCounselorToCabinById(counselor.Id, 1,
                new DateTime(2022, 01, 01), new DateTime(2022, 12, 01));

            cabinManager.AddCounselorToCabinById(counselor2.Id, 1,
                new DateTime(2022, 01, 02), new DateTime(2022, 11, 01));

            Assert.AreEqual(expectedAmountOfCounselor2,
                context.CabinCounselorStays.Where(ccs => ccs.CounselorId == counselor2.Id).Count());
        }

        [Test]
        public void AddCounselorToCabin_CounselorWasResponsibleForPreviousCabin()
        {
            int expectedAmountOfConselors = 2;
            using var context = TestAddons.GetTestContext("CounselorMultiResponsibleToCabin");
            var cabinManager = new CabinManager(context);
            cabinManager.AddCabinByName("Cabin");

            Counselor counselor = new Counselor()
            {
                FirstName = "Councelor",
                LastName = "Counselor",
                Title = "Lord",
                PhoneNumber = "010-123456"
            };
            CounselorManager counselorManager = new(context);
            counselorManager.AddCounselor(counselor);

            cabinManager.AddCounselorToCabinById(counselor.Id, 1,
                new DateTime(2022, 01, 01), new DateTime(2022, 02, 01));

            cabinManager.AddCabinByName("SecondCabin");
            cabinManager.AddCounselorToCabinById(counselor.Id, 2,
                new DateTime(2022, 02, 02), new DateTime(2022, 03, 01));

            Assert.AreEqual(expectedAmountOfConselors,
                context.CabinCounselorStays.Where(ccs => ccs.CounselorId == counselor.Id).Count());
        }

        [Test]
        public void AddCamperToCabin()
        {
            int amountOfresidents = 1;

            using var context = TestAddons.GetTestContext("AddCamper");
            var cabinManager = new CabinManager(context);
            cabinManager.AddCabinByName("Cabin");

            Counselor counselor = new Counselor()
            {
                FirstName = "Councelor",
                LastName = "Counselor",
                Title = "Lord",
                PhoneNumber = "010-123456"
            };
            CounselorManager counselorManager = new(context);
            counselorManager.AddCounselor(counselor);

            Camper camper = new Camper()
            {
                FirstName = "FN",
                LastName = "LN",
                DateOfBirth = new DateTime()
            };
            CamperManager camperManager = new(context);
            camperManager.AddCamper(camper);
            DateTime start = new DateTime(2022, 01, 22);
            DateTime end = new DateTime(2022, 02, 22);


            cabinManager.AddCounselorToCabinById(counselor.Id, 1, start, end);
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

            Counselor counselor = new Counselor()
            {
                FirstName = "Councelor",
                LastName = "Counselor",
                Title = "Lord",
                PhoneNumber = "010-123456"
            };
            CounselorManager counselorManager = new(context);
            counselorManager.AddCounselor(counselor);

            DateTime start = new DateTime(2022, 01, 22);
            DateTime end = new DateTime(2022, 02, 22);
            cabinManager.AddCounselorToCabinById(counselor.Id, 1, start, end);

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

        [Test]
        public void AddCamperToCabinWithNoCounselor()
        {
            int amountOfresidents = 0;

            using var context = TestAddons.GetTestContext("AddCamperToNonCounselorCabin");
            var cabinManager = new CabinManager(context);
            cabinManager.AddCabinByName("Cabin");

            Camper camper = new Camper()
            {
                FirstName = "FN",
                LastName = "LN",
                DateOfBirth = new DateTime()
            };
            CamperManager camperManager = new(context);
            camperManager.AddCamper(camper);
            DateTime start = new DateTime(2022, 01, 22);
            DateTime end = new DateTime(2022, 02, 22);

            cabinManager.AddCamperToCabin(camper.Id, 1, start, end);

            Assert.AreEqual(amountOfresidents, context.Cabins.First().Campers.Count);
        }

        [Test]
        public void AddCamperToCabinWithNoPresentCounselor()
        {
            int amountOfresidents = 0;

            using var context = TestAddons.GetTestContext("AddCamperToNonPresentCounselorCabin");
            var cabinManager = new CabinManager(context);
            cabinManager.AddCabinByName("Cabin");


            Counselor counselor = new Counselor()
            {
                FirstName = "Councelor",
                LastName = "Counselor",
                Title = "Lord",
                PhoneNumber = "010-123456"
            };
            CounselorManager counselorManager = new(context);
            counselorManager.AddCounselor(counselor);
            cabinManager.AddCounselorToCabinById(counselor.Id, 1,
                new DateTime(2021, 01, 1), new DateTime(2022, 01, 21));

            Camper camper = new Camper()
            {
                FirstName = "FN",
                LastName = "LN",
                DateOfBirth = new DateTime()
            };
            CamperManager camperManager = new(context);
            camperManager.AddCamper(camper);
            DateTime start = new DateTime(2022, 01, 22);
            DateTime end = new DateTime(2022, 02, 22);

            cabinManager.AddCamperToCabin(camper.Id, 1, start, end);

            Assert.AreEqual(amountOfresidents, context.Cabins.First().Campers.Count);
        }

        [Test]
        public void AddCamperWherePreviousCampersHaveLived()
        {
            int expectedTotalAmountOfresidents = 5;
            int expectedActiveAmountOfresidents = 1;

            using var context = TestAddons.GetTestContext("AddCamperWherePreviousCampersHaveLived");
            var cabinManager = new CabinManager(context);
            cabinManager.AddCabinByName("Cabin");

            Counselor counselor = new Counselor()
            {
                FirstName = "Councelor",
                LastName = "Counselor",
                Title = "Lord",
                PhoneNumber = "010-123456"
            };
            CounselorManager counselorManager = new(context);
            counselorManager.AddCounselor(counselor);
            cabinManager.AddCounselorToCabinById(counselor.Id, 1,
                new DateTime(2021, 01, 1), new DateTime(2022, 01, 21));

            DateTime oldStart = new DateTime(2021, 01, 02);
            DateTime oldEnd = new DateTime(2021, 02, 01);
            CamperManager camperManager = new(context);
            for (int camperIndex = 0; camperIndex < 4; camperIndex++)
            {
                Camper camper = new Camper()
                {
                    FirstName = "FN",
                    LastName = "LN",
                    DateOfBirth = new DateTime()
                };
                camperManager.AddCamper(camper);
                cabinManager.AddCamperToCabin(camper.Id, 1, oldStart, oldEnd);
            }

            DateTime newStart = new DateTime(2021, 04, 02);
            DateTime newEnd = new DateTime(2021, 05, 01);
            Camper newCamper = new Camper()
            {
                FirstName = "New",
                LastName = "New",
                DateOfBirth = new DateTime()
            };
            camperManager.AddCamper(newCamper);
            cabinManager.AddCamperToCabin(newCamper.Id, 1, newStart, newEnd);

            Assert.AreEqual(expectedTotalAmountOfresidents, context.Cabins.First().Campers.Count);
            Assert.AreEqual(expectedActiveAmountOfresidents,
                cabinManager.GetActiveCampersInCabinById(1, new DateTime(2021, 04, 10)).Count());
        }
    }
}
