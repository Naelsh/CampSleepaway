using CampSleepaway.Application.Cabins;
using CampSleepaway.Application.Campers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampSleepaway.Test.Units.Application
{
    class CamperTest
    {
        [Test]
        public void AddCamperByName_EmptyString_NoChange()
        {
            int expected = 0;

            using var context = TestAddons.GetTestContext("CanNotInsertCamper");
            var cabinCreate = new CamperManager(context);

            int result = cabinCreate.AddCamperByName(null, null, new DateTime());

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void CanInsertCAmperIntoDatabase()
        {
            var expected = 1;

            using var context = TestAddons.GetTestContext("CanInsertCamper");
            var cabinCreate = new CamperManager(context);

            int result = cabinCreate.AddCamperByName("FirstName", "LastName", new DateTime(2012,01,01));

            Assert.AreEqual(expected, result);
        }
    }
}
