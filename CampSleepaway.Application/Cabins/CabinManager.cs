using CampSleepaway.Application.Counselors;
using CampSleepaway.Domain.Data;
using CampSleepaway.Persistence;
using System;
using System.Linq;

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
            Cabin cabin = new() { Name = name };
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
            if (!CabinHasCouncelor(cabinId, startDate, endDate)) { return 0; }
            _context.CabinCamperStays.Add(new CabinCamperStay()
            {
                CabinId = cabinId,
                CamperId = camperId,
                StartTime = startDate,
                EndTime = endDate
            });
            return _context.SaveChanges();
        }

        public int AddCounselorToCabinById(int counselorId, int cabinId, DateTime start, DateTime end)
        {
            if (CabinHasCouncelor(cabinId, start, end)) { return 0; }
            if (!IsCouncelorAvailableForNewCabin(counselorId, start, end)) { return 0; }
            _context.CabinCounselorStays.Add(new CabinCounselorStay()
            {
                CounselorId = counselorId,
                CabinId = cabinId,
                StartTime = start,
                EndTime = end
            });
            return _context.SaveChanges();
        }

        private bool IsCouncelorAvailableForNewCabin(int counselorId, DateTime start, DateTime end)
        {
            int amountInSpan =
                _context.CabinCounselorStays.Where(ccs => ccs.CounselorId == counselorId)
                .Where(ccs => (ccs.StartTime <= start && start <= ccs.EndTime)
                || (ccs.StartTime <= end && end <= ccs.EndTime))
                .Count();
            return (amountInSpan) == 0;
        }

        // Gives true if there is a councelor when the campers enter the cabin
        // (could be removed while cabin is used)
        private bool CabinHasCouncelor(int cabinId, DateTime start, DateTime end)
        {
            int amountInSpan =
                _context.CabinCounselorStays.Where(ccs => ccs.CabinId == cabinId)
                .Where(ccs => ccs.StartTime <= start && start <= ccs.EndTime)
                .Count();
            return amountInSpan > 0;
        }

        private bool IsCabinFull(int cabinId)
        {
            int maxAmount = 4;
            int amountInCabin = _context.CabinCamperStays.Where(ccs => ccs.CabinId == cabinId).Count();
            return amountInCabin >= maxAmount;
        }
    }
}
