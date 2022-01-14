using System.Collections.Generic;

namespace CampSleepaway.Domain
{
    public class Camper : Person
    {
        public int Age { get; set; }
        public List<Cabin> CabinStays { get; set; } = new List<Cabin>();
        public List<NextOfKin> NextOfKins { get; set; } = new List<NextOfKin>();
        public List<Visit> Visits { get; set; } = new List<Visit>();
    }
}
