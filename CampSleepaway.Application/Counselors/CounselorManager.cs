using CampSleepaway.Domain.Data;
using CampSleepaway.Persistence;
using System.Collections.Generic;
using System.Linq;

namespace CampSleepaway.Application.Counselors
{
    public class CounselorManager : ManagerCore
    {
        public CounselorManager(CampSleepawayContext context) : base(context)
        {
        }

        public int AddCounselor(Counselor counselor)
        {
            if (counselor.FirstName == null) { return 0; }
            if (counselor.LastName == null) { return 0; }
            _context.Counselors.Add(counselor);
            var result = _context.SaveChanges();
            return result;
        }

        public List<Counselor> GetAllItems()
        {
            return _context.Counselors.ToList();
        }

        public Counselor GetById(int id)
        {
            return _context.Counselors.FirstOrDefault(c => c.Id == id);
        }
    }
}
