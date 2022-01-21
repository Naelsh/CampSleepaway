using System;
using System.ComponentModel.DataAnnotations;

namespace CampSleepaway.Domain.Data
{
    public class CabinCamperStay
    {
        [Key]
        public int CamperId { get; set; }
        [Key]
        public int CabinId { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
    }
}
