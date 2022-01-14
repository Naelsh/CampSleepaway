using System;

namespace CampSleepaway.Domain
{
    public class Visit
    {
        public int NextOfKinId { get; set; }
        public int CamperId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
