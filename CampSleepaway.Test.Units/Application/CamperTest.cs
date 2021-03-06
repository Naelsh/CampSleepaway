using CampSleepaway.Application;
using CampSleepaway.Domain;
using NUnit.Framework;

namespace CampSleepaway.Test.Units.Application
{
    class CamperTest
    {
        [Test]
        public void AddCamperByName_EmptyString_NoChange()
        {
            int expected = 0;

            using var context = TestAddons.GetTestContext("CanNotInsertCamper");
            var camperManager = new CamperManager(context);
            var camper = new Camper();

            int result = camperManager.AddCamper(camper);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void CanInsertCamperIntoDatabase()
        {
            var expected = 1;

            using var context = TestAddons.GetTestContext("CanInsertCamper");
            var camperManager = new CamperManager(context);
            var camper = new Camper()
            {
                FirstName = "FirstName",
                LastName = "LastName"
            };

            int result = camperManager.AddCamper(camper);

            Assert.AreEqual(expected, result);
        }

        //[Test]
        //public void AddRelationBetweenCamperAndNextOfKin()
        //{
        //    var expected = 1;

        //    using var context = TestAddons.GetTestContext("AddRelations");
        //    var camperManager = new CamperManager(context);
        //    Camper camper = new Camper()
        //    {
        //        FirstName = "FirstName",
        //        LastName = "LastName"
        //    };
        //}
    }
}
