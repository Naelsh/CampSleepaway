using CampSleepaway.Application;
using CampSleepaway.Domain;
using NUnit.Framework;

namespace CampSleepaway.Test.Units.Application
{
    class CounselorTest
    {
        [Test]
        public void AddCounselor_NoProperties_NoChange()
        {
            int expectedChanges = 0;
            using var context = TestAddons.GetTestContext("Counselor");
            var manager = new CounselorManager(context);

            Counselor counselor = new ();
            int result = manager.AddCounselor(counselor);

            Assert.AreEqual(expectedChanges, result);
        }

        [Test]
        public void AddCounselor_AllRequiredProperties_AddEntry()
        {
            int expectedChanges = 1;
            using var context = TestAddons.GetTestContext("Counselor");
            var manager = new CounselorManager(context);

            Counselor counselor = new ()
            {
                FirstName = "FirstName",
                LastName = "LastName",
                PhoneNumber = "PhoneNum",
                Title = "Title"
            };

            int result = manager.AddCounselor(counselor);

            Assert.AreEqual(expectedChanges, result);
        }

        [Test]
        public void AddCounselor_MissingLastName_NoChange()
        {
            int expectedChanges = 0;
            using var context = TestAddons.GetTestContext("Counselor");
            var manager = new CounselorManager(context);

            Counselor counselor = new ()
            {
                FirstName = "FirstName",
                PhoneNumber = "PhoneNum",
                Title = "Title"
            };

            int result = manager.AddCounselor(counselor);

            Assert.AreEqual(expectedChanges, result);
        }

        [Test]
        public void AddCounselor_PhoneNumber_AddOne()
        {
            int expectedChanges = 1;
            using var context = TestAddons.GetTestContext("Counselor");
            var manager = new CounselorManager(context);

            Counselor counselor = new ()
            {
                FirstName = "FirstName",
                LastName = "LastName",
                Title = "Title"
            };

            int result = manager.AddCounselor(counselor);

            Assert.AreEqual(expectedChanges, result);
        }
    }
}
