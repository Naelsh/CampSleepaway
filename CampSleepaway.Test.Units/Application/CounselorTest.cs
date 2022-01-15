using CampSleepaway.Application.Counselors;
using CampSleepaway.Domain.Data;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            Counselor counselor = new Counselor();
            int result = manager.AddCounselor(counselor);

            Assert.AreEqual(expectedChanges, result);
        }

        [Test]
        public void AddCounselor_AllRequiredProperties_AddEntry()
        {
            int expectedChanges = 1;
            using var context = TestAddons.GetTestContext("Counselor");
            var manager = new CounselorManager(context);

            Counselor counselor = new Counselor()
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

            Counselor counselor = new Counselor()
            {
                FirstName = "FirstName",
                PhoneNumber = "PhoneNum",
                Title = "Title"
            };

            int result = manager.AddCounselor(counselor);

            Assert.AreEqual(expectedChanges, result);
        }

        [Test]
        public void AddCounselor_PhoneNumber_NoChange()
        {
            int expectedChanges = 0;
            using var context = TestAddons.GetTestContext("Counselor");
            var manager = new CounselorManager(context);

            Counselor counselor = new Counselor()
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
