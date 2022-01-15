using CampSleepaway.Domain.Data;
using CampSleepaway.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampSleepaway.Application.Cabins
{
    public class CabinManager
    {
        private readonly CampSleepawayContext _context;

        public CabinManager(CampSleepawayContext context)
        {
            _context = context;
        }

        public int AddCabinByName(string name)
        {
            if (name == null)
            {
                return 0;
            }
            Cabin cabin = new () { Name = name};
            _context.Cabins.Add(cabin);
            var result = _context.SaveChanges();
            return result;
        }
    }
}
