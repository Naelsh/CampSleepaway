using System;

namespace CampSleepaway.Domain
{
    public class CabinCounselorStay
    {
        public int CounselorId { get; set; }
        public int CabinId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
