using CampSleepaway.Domain.Data;
using CampSleepaway.Persistence;

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
            if (nextOfKin.Camper.Count == 0) { return 0; }

            // a strange solution, but in order to setup proper m2m relationship
            // I make sure that EF Core does not get to add the relationship
            int camperId = nextOfKin.Camper[0].Id;
            nextOfKin.Camper.Clear();

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
    }
}
