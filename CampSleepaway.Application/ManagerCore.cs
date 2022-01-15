using CampSleepaway.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampSleepaway.Application
{
    public abstract class ManagerCore
    {
        internal readonly CampSleepawayContext _context;

        public ManagerCore(CampSleepawayContext context)
        {
            _context = context;
        }
    }
}
