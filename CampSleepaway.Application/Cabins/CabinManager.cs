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
            var result = _context.SaveChanges();
            return result;
        }

        public Cabin GetCabinById(int id)
        {
            var result = _context.Cabins.Where(x => x.Id == id);
            return result.First();
        }

        public int AddCamperToCabin(int camperId, int cabinId)
        {
            Camper camper = _context.Campers.Where(x => x.Id == camperId).First();
            Cabin cabin = _context.Cabins.Where(x => x.Id == cabinId).First();
            return 1;
        }
        
    }
}
