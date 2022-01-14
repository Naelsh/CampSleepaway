using System.Collections.Generic;

namespace CampSleepaway.Domain
{
    public class Camper
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public List<Cabin> CabinStays { get; set; } = new List<Cabin>();
        public List<NextOfKin> NextOfKins { get; set; } = new List<NextOfKin>();
        public List<Visit> Visits { get; set; } = new List<Visit>();
    }
}
