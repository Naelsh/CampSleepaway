using CampSleepaway.Domain.Data;
using CampSleepaway.Persistence;

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
    }
}
