using CampSleepaway.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampSleepaway.Application
{
    public static class ClearDatabase
    {
        public static void Clear()
        {
            var context = new CampSleepawayContext();
            foreach (var item in context.Cabins)
            {
                context.Cabins.Remove(item);
            }
            foreach (var item in context.Campers)
            {
                context.Campers.Remove(item);
            }
            foreach (var item in context.Counselors)
            {
                context.Counselors.Remove(item);
            }
            foreach (var item in context.NextOfKins)
            {
                context.NextOfKins.Remove(item);
            }
            context.SaveChanges();
        }
    }
}
