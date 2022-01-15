using CampSleepaway.Domain.Data;
using CampSleepaway.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampSleepaway.Application.NextOfKins
{
    public class NextOfKinManager : ManagerCore
    {
        public NextOfKinManager(CampSleepawayContext context) : base(context)
        {
        }

        public int AddNextOfKin(NextOfKin nextOfKin)
        {
            if (nextOfKin.FirstName == null) { return 0; }
            if (nextOfKin.LastName == null) { return 0; }
            if (nextOfKin.Children.Count == 0) { return 0; }

            _context.NextOfKins.Add(nextOfKin);
            var result = _context.SaveChanges();
            return result;
        }
    }
}
