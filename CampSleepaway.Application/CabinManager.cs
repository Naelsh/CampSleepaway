using CampSleepaway.Domain;
using CampSleepaway.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CampSleepaway.Application
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
            if (IsCabinFull(cabinId, startDate)) { return 0; }
            if (!CabinHasActiveCouncelor(cabinId, startDate, endDate)) { return 0; }
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
            if (CabinHasActiveCouncelor(cabinId, start, end)) { return 0; }
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
                .Where(ccs => ccs.StartTime <= start && start <= ccs.EndTime
                || ccs.StartTime <= end && end <= ccs.EndTime)
                .Count();
            return amountInSpan == 0;
        }

        public Counselor GetCounselorInCabinById(int cabinId, DateTime date)
        {
            return (from counselor in _context.Counselors
                    join ccs in _context.CabinCounselorStays on counselor.Id equals ccs.CounselorId
                    where ccs.CabinId == cabinId && ccs.StartTime <= date && date <= ccs.EndTime
                    select counselor).FirstOrDefault();
        }

        // Gives true if there is a councelor when the campers enter the cabin
        // (could be removed while cabin is used)
        private bool CabinHasActiveCouncelor(int cabinId, DateTime start, DateTime end)
        {
            int amountInSpan =
                _context.CabinCounselorStays.Where(ccs => ccs.CabinId == cabinId)
                .Where(ccs => ccs.StartTime <= start && start <= ccs.EndTime)
                .Count();
            return amountInSpan > 0;
        }

        private bool IsCabinFull(int cabinId, DateTime date)
        {
            int maxAmount = 4;
            int amountInCabin = GetActiveCampersInCabinById(cabinId, date).Count();
            return amountInCabin >= maxAmount;
        }

        public IQueryable<Camper> GetActiveCampersInCabinById(int cabinId, DateTime date)
        {
            return from camper in _context.Campers
                   join ccs in _context.CabinCamperStays on camper.Id equals ccs.CamperId
                   where ccs.CabinId == cabinId && ccs.StartTime <= date && date <= ccs.EndTime
                   select camper;
        }

        public Cabin GetById(int id)
        {
            return _context.Cabins.FirstOrDefault(c => c.Id == id);
        }

        public List<Cabin> GetAllItems()
        {
            return _context.Cabins.ToList();
        }
    }
}
