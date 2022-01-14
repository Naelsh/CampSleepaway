using System.Collections.Generic;

namespace CampSleepaway.Domain
{
    public class Counselor : Person
    {
        public string PhoneNumber { get; set; }
        public string Title { get; set; }
        public List<Cabin> CabinStays { get; set; } = new List<Cabin>();
    }
}
