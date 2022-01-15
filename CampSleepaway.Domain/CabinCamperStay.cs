using System;

namespace CampSleepaway.Domain
{
    public class CabinCamperStay
    {
        public int CamperId { get; set; }
        public int CabinId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
