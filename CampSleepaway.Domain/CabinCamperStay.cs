using System;

namespace CampSleepaway.Domain
{
    public class CabinCamperStay
    {
        public int CamperId { get; set; }
        public int CabinId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
