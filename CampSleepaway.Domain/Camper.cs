using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CampSleepaway.Domain
{
    public class Camper
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        [Required]
        public string FirstName { get; set; }
        [MaxLength(50)]
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        public List<Cabin> CabinStays { get; set; } = new List<Cabin>();
        public List<NextOfKin> NextOfKins { get; set; } = new List<NextOfKin>();
        public List<Visit> Visits { get; set; } = new List<Visit>();
    }
}
