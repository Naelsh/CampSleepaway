using CampSleepaway.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampSleepaway.Test.Units.Application
{
    class TestAddons
    {
        internal static CampSleepawayContext GetTestContext(string testName)
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase(testName);
            return new CampSleepawayContext(builder.Options);
        }
    }
}
