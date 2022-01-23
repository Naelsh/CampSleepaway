using CampSleepaway.Domain.Data;
using CampSleepaway.Persistence;
using System.Collections.Generic;
using System.Linq;

namespace CampSleepaway.Application.NextOfKins
{
    public class NextOfKinManager : ManagerCore
    {
        public NextOfKinManager(CampSleepawayContext context) : base(context)
        {
        }

        public int AddNextOfKin(NextOfKin nextOfKin, string relationship)
        {
            if (nextOfKin.FirstName == null) { return 0; }
            if (nextOfKin.LastName == null) { return 0; }
            if (nextOfKin.Campers.Count == 0) { return 0; }

            // a strange solution, but in order to setup proper m2m relationship
            // I make sure that EF Core does not get to add the relationship
            int camperId = nextOfKin.Campers[0].Id;
            nextOfKin.Campers.Clear();

            _context.NextOfKins.Add(nextOfKin);
            var result = _context.SaveChanges();
            
            result += AddNextOfKinRelationship(nextOfKin.Id, camperId, relationship);

            return result;
        }

        public int AddNextOfKinRelationship(int nextOfKinId, int childId, string relationship)
        {
            _context.CamperNextOfKins.Add(new CamperNextOfKin()
            {
                NextOfKinId = nextOfKinId,
                CamperId = childId,
                NextOfKinRelationship = relationship
            });
            return _context.SaveChanges();
        }

        public List<NextOfKin> GetAllItems()
        {
            return _context.NextOfKins.ToList();
        }

        public NextOfKin GetById(int id)
        {
            return _context.NextOfKins.FirstOrDefault(nok => nok.Id == id);
        }

        internal List<int> GetIdOfAllCampersNextOfKinHasRelationshipWith(int nextOfKinId)
        {
            return (from campers in _context.Campers
                    join relations in _context.CamperNextOfKins on campers.Id equals relations.CamperId
                    where relations.NextOfKinId == nextOfKinId
                    select  campers.Id ).ToList();
        }
    }
}
