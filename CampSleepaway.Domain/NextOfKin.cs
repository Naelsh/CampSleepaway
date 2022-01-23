using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CampSleepaway.Domain
{
    public class NextOfKin
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        [Required]
        public string FirstName { get; set; }
        [MaxLength(50)]
        [Required]
        public string LastName { get; set; }
        [MaxLength(12)]
        public string PhoneNumber { get; set; }
        [MaxLength(50)]
        [Required]
        public string MailAddress { get; set; }
        public List<Camper> Campers { get; set; } = new List<Camper>();
        public List<Visit> Visits { get; set; } = new List<Visit>();
    }
}
