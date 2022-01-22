using CampSleepaway.Application.Cabins;
using CampSleepaway.Application.NextOfKins;
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

        public List<Camper> GetAllItems()
        {
            return _context.Campers.ToList();
        }

        public Camper GetById(int id)
        {
            return _context.Campers.FirstOrDefault(c => c.Id == id);
        }

        public List<CabinCamperView> GetAllCamperAndNextOfKinRelationsOrderedByCabin()
        {
            var result = from cabins in _context.Cabins
                         join cabinCamperStay in _context.CabinCamperStays on cabins.Id equals cabinCamperStay.CabinId
                         join camper in _context.Campers on cabinCamperStay.CamperId equals camper.Id
                         join camperNextOfKin in _context.CamperNextOfKins on camper.Id equals camperNextOfKin.CamperId
                         join nextOfKin in _context.NextOfKins on camperNextOfKin.NextOfKinId equals nextOfKin.Id
                         select new CabinCamperView
                         {
                             CabinId = cabins.Id,
                             CabinName = cabins.Name,
                             CamperId = camper.Id,
                             CamperFirstName = camper.FirstName,
                             CamperLastName = camper.LastName,
                             NextOfKinId = nextOfKin.Id,
                             NextOfKinFirstName = nextOfKin.FirstName,
                             NextOfKinLastName = nextOfKin.LastName,
                             Relationship = camperNextOfKin.NextOfKinRelationship
                       };
            return result.ToList();
        }
    }

    public class CabinCamperView
    {
        public int CabinId { get; set; }
        public string CabinName { get; set; }
        public int CamperId { get; set; }
        public string CamperFirstName { get; set; }
        public string CamperLastName { get; set; }
        public int NextOfKinId { get; set; }
        public string NextOfKinFirstName { get; set; }
        public string NextOfKinLastName { get; set; }
        public string Relationship { get; set; }
    }
}
