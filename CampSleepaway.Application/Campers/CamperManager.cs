using CampSleepaway.Domain.Data;
using CampSleepaway.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampSleepaway.Application.Campers
{
    public class CamperManager
    {
        private readonly CampSleepawayContext _context;

        public CamperManager(CampSleepawayContext context)
        {
            _context = context;
        }

        public int AddCamperByName(string firstName, string lastName, DateTime dateOfBirth)
        {
            if (firstName == null || lastName == null)
            {
                return 0;
            }
            Camper camper = new() {FirstName = firstName, LastName = lastName, DateOfBirth = dateOfBirth };
            _context.Campers.Add(camper);
            var result = _context.SaveChanges();
            return result;
        }
    }
}
