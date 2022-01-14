using System;

namespace CampSleepaway.Domain
{
    public class CabinStay
    {
        public int PersonId { get; set; }
        public int CabinId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
