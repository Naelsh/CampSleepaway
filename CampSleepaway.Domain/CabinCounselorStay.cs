using System;

namespace CampSleepaway.Domain
{
    public class CabinCounselorStay
    {
        public int CounselorId { get; set; }
        public int CabinId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
