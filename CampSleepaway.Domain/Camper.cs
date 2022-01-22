using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CampSleepaway.Domain.Data
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
    }
}
