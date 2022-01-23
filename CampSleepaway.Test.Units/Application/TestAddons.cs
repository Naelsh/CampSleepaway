using CampSleepaway.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CampSleepaway.Test.Units.Application
{
    static class TestAddons
    {
        internal static CampSleepawayContext GetTestContext(string testName)
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase(testName);
            return new CampSleepawayContext(builder.Options);
        }
    }
}
