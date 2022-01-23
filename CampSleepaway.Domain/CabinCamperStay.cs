using System;
using System.ComponentModel.DataAnnotations;

namespace CampSleepaway.Domain
{
    public class CabinCamperStay
    {
        public int CamperId { get; set; }
        public int CabinId { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
    }
}
