using CampSleepaway.Domain.Data;
using CampSleepaway.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampSleepaway.Application.Cabins
{
    public class CabinManager : ManagerCore
    {
        public CabinManager(CampSleepawayContext context) : base(context)
        { }

        public int AddCabinByName(string name)
        {
            if (name == null)
            {
                return 0;
            }
            Cabin cabin = new () { Name = name};
            _context.Cabins.Add(cabin);
            return _context.SaveChanges();
        }

        public Cabin GetCabinById(int id)
        {
            var result = _context.Cabins.Where(x => x.Id == id);
            return result.First();
        }

        public int AddCamperToCabin(int camperId, int cabinId, DateTime startDate, DateTime endDate)
        {
            if (IsCabinFull(cabinId)) { return 0; }
            _context.CabinCamperStays.Add(new CabinCamperStay()
            {
                CabinId = cabinId,
                CamperId = camperId,
                StartTime = startDate,
                EndTime = endDate
            });
            return _context.SaveChanges();
        }

        private bool IsCabinFull(int cabinId)
        {
            int maxAmount = 4;
            int amountInCabin = _context.CabinCamperStays.Where(ccs => ccs.CabinId == cabinId).Count();
            return amountInCabin >= maxAmount;
        }
    }
}
