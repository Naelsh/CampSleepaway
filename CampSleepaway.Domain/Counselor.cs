using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CampSleepaway.Domain
{
    public class Counselor
    {
        public int Id { get; set; }
        [MaxLength(50)]
        [Required]
        public string FirstName { get; set; }
        [MaxLength(50)]
        [Required]
        public string LastName { get; set; }
        [MaxLength(12)]
        public string PhoneNumber { get; set; }
        [MaxLength(25)]
        public string Title { get; set; }
        public List<Cabin> CabinStays { get; set; } = new List<Cabin>();
    }
}
