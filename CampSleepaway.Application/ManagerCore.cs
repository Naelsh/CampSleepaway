using CampSleepaway.Persistence;

namespace CampSleepaway.Application
{
    public abstract class ManagerCore
    {
        internal readonly CampSleepawayContext _context;

        public ManagerCore(CampSleepawayContext context)
        {
            _context = context;
        }
    }
}
