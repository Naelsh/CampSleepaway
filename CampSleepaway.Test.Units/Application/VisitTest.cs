using CampSleepaway.Application;
using CampSleepaway.Domain;
using NUnit.Framework;
using System;

namespace CampSleepaway.Test.Units.Application
{
    class VisitTest
    {
        [Test]
        public void CreateVisitWithinTimeFrameSingleNextOfKin()
        {
            int expectedChanges = 2;

            using var context = TestAddons.GetTestContext("CreateVisitWithinTimeFrameSingleNextOfKin");

            Camper camper = new Camper()
            {
                FirstName = "FN",
                LastName = "LN",
                DateOfBirth = new DateTime(2010, 01, 01)
            };
            CamperManager camperManager = new(context);
            camperManager.AddCamper(camper);

            NextOfKin nextOfKin = new()
            {
                FirstName = "NFN",
                LastName = "NLN",
                PhoneNumber = "0191911",
                MailAddress = "test@test.com"
            };
            nextOfKin.Campers.Add(camper);
            NextOfKinManager nextOfKinManager = new(context);
            nextOfKinManager.AddNextOfKin(nextOfKin, "Guardian");

            DateTime startTime = new DateTime(2022, 01, 01, 10, 00, 00);
            int duration = 120;
            VisitManager visitManager = new(context);
            int result = visitManager.AddNewVisit(camper.Id, startTime, duration, nextOfKin.Id);

            Assert.AreEqual(expectedChanges, result);
        }

        [Test]
        public void CreateVisitWithinTimeFrameMultipleNextOfKin()
        {
            int expectedChanges = 3;

            using var context = TestAddons.GetTestContext("CreateVisitWithinTimeFrameMultipleNextOfKin");

            Camper camper = new Camper()
            {
                FirstName = "FN",
                LastName = "LN",
                DateOfBirth = new DateTime(2010, 01, 01)
            };
            CamperManager camperManager = new(context);
            camperManager.AddCamper(camper);

            NextOfKinManager nextOfKinManager = new(context);
            NextOfKin nextOfKin1 = new()
            {
                FirstName = "NFN",
                LastName = "NLN",
                PhoneNumber = "0191911",
                MailAddress = "test@test.com"
            };
            nextOfKin1.Campers.Add(camper);
            nextOfKinManager.AddNextOfKin(nextOfKin1, "Guardian");
            
            NextOfKin nextOfKin2 = new()
            {
                FirstName = "NFN",
                LastName = "NLN",
                PhoneNumber = "0191911",
                MailAddress = "test@test.com"
            };
            nextOfKin2.Campers.Add(camper);
            nextOfKinManager.AddNextOfKin(nextOfKin2, "Guardian");

            DateTime startTime = new DateTime(2022, 01, 01, 10, 00, 00);
            int duration = 120;
            VisitManager visitManager = new(context);
            int result = visitManager.AddNewVisit(camper.Id, startTime, duration, new int[] { nextOfKin1.Id, nextOfKin2.Id });

            Assert.AreEqual(expectedChanges, result);
        }

        [Test]
        public void CreateVisitOutsideTimeFrameStartToEarly()
        {
            int expectedChanges = 0;

            using var context = TestAddons.GetTestContext("CreateVisitOutsideTimeFrameStartToEarly");

            Camper camper = new Camper()
            {
                FirstName = "FN",
                LastName = "LN",
                DateOfBirth = new DateTime(2010, 01, 01)
            };
            CamperManager camperManager = new(context);
            camperManager.AddCamper(camper);

            NextOfKinManager nextOfKinManager = new(context);
            NextOfKin nextOfKin1 = new()
            {
                FirstName = "NFN",
                LastName = "NLN",
                PhoneNumber = "0191911",
                MailAddress = "test@test.com"
            };
            nextOfKin1.Campers.Add(camper);
            nextOfKinManager.AddNextOfKin(nextOfKin1, "Guardian");

            DateTime startTime = new DateTime(2022, 01, 01, 09, 00, 00);
            int duration = 120;
            VisitManager visitManager = new(context);
            int result = visitManager.AddNewVisit(camper.Id, startTime, duration, new int[] { nextOfKin1.Id });

            Assert.AreEqual(expectedChanges, result);
        }

        [Test]
        public void CreateVisitOutsideTimeFrameEndToLate()
        {
            int expectedChanges = 0;

            using var context = TestAddons.GetTestContext("CreateVisitOutsideTimeFrameEndToLate");

            Camper camper = new Camper()
            {
                FirstName = "FN",
                LastName = "LN",
                DateOfBirth = new DateTime(2010, 01, 01)
            };
            CamperManager camperManager = new(context);
            camperManager.AddCamper(camper);

            NextOfKinManager nextOfKinManager = new(context);
            NextOfKin nextOfKin1 = new()
            {
                FirstName = "NFN",
                LastName = "NLN",
                PhoneNumber = "0191911",
                MailAddress = "test@test.com"
            };
            nextOfKin1.Campers.Add(camper);
            nextOfKinManager.AddNextOfKin(nextOfKin1, "Guardian");

            DateTime startTime = new DateTime(2022, 01, 01, 19, 00, 00);
            int durationInMinutes = 120;
            VisitManager visitManager = new(context);
            int result = visitManager.AddNewVisit(camper.Id, startTime, durationInMinutes, new int[] { nextOfKin1.Id });

            Assert.AreEqual(expectedChanges, result);
        }

        [Test]
        public void CreateVisitDurationToLong()
        {
            int expectedChanges = 0;

            using var context = TestAddons.GetTestContext("CreateVisitDurationToLong");

            Camper camper = new Camper()
            {
                FirstName = "FN",
                LastName = "LN",
                DateOfBirth = new DateTime(2010, 01, 01)
            };
            CamperManager camperManager = new(context);
            camperManager.AddCamper(camper);

            NextOfKinManager nextOfKinManager = new(context);
            NextOfKin nextOfKin1 = new()
            {
                FirstName = "NFN",
                LastName = "NLN",
                PhoneNumber = "0191911",
                MailAddress = "test@test.com"
            };
            nextOfKin1.Campers.Add(camper);
            nextOfKinManager.AddNextOfKin(nextOfKin1, "Guardian");

            DateTime startTime = new DateTime(2022, 01, 01, 10, 00, 00);
            int durationInMinutes = 181;
            VisitManager visitManager = new(context);
            int result = visitManager.AddNewVisit(camper.Id, startTime, durationInMinutes, new int[] { nextOfKin1.Id });

            Assert.AreEqual(expectedChanges, result);
        }

        [Test]
        public void CreateVisit_NextOfKinNotResponsible()
        {
            int expectedChanges = 0;

            using var context = TestAddons.GetTestContext("CreateVisit_NextOfKinNotResponsible");

            CamperManager camperManager = new(context);
            Camper nextOfKinCamper = new Camper()
            {
                FirstName = "FN",
                LastName = "LN",
                DateOfBirth = new DateTime(2010, 01, 01)
            };
            camperManager.AddCamper(nextOfKinCamper);

            Camper noConnectionCamper = new Camper()
            {
                FirstName = "NoRelation",
                LastName = "Seriously",
                DateOfBirth = new DateTime(2010, 2, 2)
            };
            camperManager.AddCamper(noConnectionCamper);

            NextOfKinManager nextOfKinManager = new(context);
            NextOfKin nextOfKin1 = new()
            {
                FirstName = "NFN",
                LastName = "NLN",
                PhoneNumber = "0191911",
                MailAddress = "test@test.com"
            };
            nextOfKin1.Campers.Add(nextOfKinCamper);
            nextOfKinManager.AddNextOfKin(nextOfKin1, "Guardian");

            DateTime startTime = new DateTime(2022, 01, 01, 10, 00, 00);
            int durationInMinutes = 120;
            VisitManager visitManager = new(context);
            int result = visitManager.AddNewVisit(noConnectionCamper.Id, startTime, durationInMinutes, new int[] { nextOfKin1.Id });

            Assert.AreEqual(expectedChanges, result);
        }
    }
}
