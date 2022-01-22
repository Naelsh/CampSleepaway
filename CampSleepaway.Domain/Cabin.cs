using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CampSleepaway.Domain.Data
{
    public class Cabin
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }
        public List<Camper> Campers { get; set; } = new List<Camper>();
        public List<Counselor> Counselors { get; set; } = new List<Counselor>();
    }
}
