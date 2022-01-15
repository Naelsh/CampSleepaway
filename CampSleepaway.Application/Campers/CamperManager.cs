using CampSleepaway.Domain.Data;
using CampSleepaway.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampSleepaway.Application.Campers
{
    public class CamperManager : ManagerCore
    {
        public CamperManager(CampSleepawayContext context) : base(context)
        { }

        public int AddCamper(Camper newCamper)
        {
            if (newCamper.FirstName == null) { return 0; }
            if (newCamper.LastName == null) { return 0; }
            _context.Campers.Add(newCamper);
            var result = _context.SaveChanges();
            return result;
        }

        //public int AddNextOfKinToCamper(Camper camper, NextOfKin nextOfKins, string relationship)
        //{
        //    return 0;
        //}
    }
}
