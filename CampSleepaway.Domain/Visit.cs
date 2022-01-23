using System;
using System.Collections.Generic;

namespace CampSleepaway.Domain
{
    public class Visit
    {
        public int Id { get; set; }
        public int CamperId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public List<NextOfKin> NextOfKins { get; set; } = new List<NextOfKin>();
    }
}
