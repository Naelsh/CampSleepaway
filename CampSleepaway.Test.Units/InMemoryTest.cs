using CampSleepaway.Domain.Data;
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
            var expected = EntityState.Added;

            var builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase("CanInsertCabin");
            using var context = new CampSleepawayContext(builder.Options);

            var cabin = new Cabin() { Name = "Cabin 1" };
            context.Cabins.Add(cabin);

            Assert.AreEqual(expected, context.Entry(cabin).State);
        }

    }
}
