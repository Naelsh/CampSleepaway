using System;
using System.ComponentModel.DataAnnotations;

namespace CampSleepaway.Domain
{
    public class CabinCounselorStay
    {
        public int CounselorId { get; set; }
        public int CabinId { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
    }
}
