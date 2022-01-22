using CampSleepaway.Domain.Data;
using CampSleepaway.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public List<Camper> GetAllCampers()
        {
            return _context.Campers.Select(x => x).ToList();
        }

        public List<Camper> GetCampersByName(string firstName)
        {
            return _context.Campers.Where(x => x.FirstName == firstName).ToList();
        }

        public Camper GetCamperById(int id)
        {
            throw new NotImplementedException();
        }

        //public int AddNextOfKinToCamper(Camper camper, NextOfKin nextOfKins, string relationship)
        //{
        //    return 0;
        //}
    }
}
