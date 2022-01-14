using System.Collections.Generic;

namespace CampSleepaway.Domain
{
    public class Counselor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Title { get; set; }
        public List<Cabin> CabinStays { get; set; } = new List<Cabin>();
    }
}
