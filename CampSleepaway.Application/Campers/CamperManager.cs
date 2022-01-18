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

        public List<Camper> GetAllCampers()
        {
            return _context.Campers.Select(x => x).OrderBy(x => x.Id).ToList();
        }

        public List<Camper> GetCampersByName(string firstName)
        {
            return _context.Campers.Where(x => x.FirstName == firstName).ToList();
        }

        public Camper GetCamperById(int id)
        {
            throw new NotImplementedException();
        }

        public int AddCamperToCabin(int camperId, int cabinId)
        {
            var cabin = _context.Cabins.Where(x => x.Id == cabinId).FirstOrDefault();
            var camper = _context.Campers.Where(x => x.Id == camperId).FirstOrDefault();
            camper.CabinStays.Add(cabin);
            var result = _context.SaveChanges();
            return result;
        }

        public int AddNextOfKinToCamper(int camperId, int nextOfKinId, string relationship)
        {
            var nextOfKin = _context.NextOfKins.Where(x => x.Id == nextOfKinId).FirstOrDefault();
            var camper = _context.Campers.Where(x => x.Id == camperId).FirstOrDefault();
            camper.NextOfKins.Add(nextOfKin);
            var result = _context.SaveChanges();
            return result;
        }
    }
}
