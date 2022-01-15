using CampSleepaway.Persistence;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace CampSleepaway.Test.Units
{
    class InMemoryTest
    {
        [Test]
        public void CanInsertCabinIntoDatabase()
        {
            var expected = 1;

            var builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase("CanInsertCabin");
            using var context = new CampSleepawayContext(builder.Options);

            var cabinLogic = new CabinLogic(context);

            string name = "Cabin 1";

            int result = cabinLogic.AddCabinByName(name);

            Assert.AreEqual(expected, result);
        }

    }
}
