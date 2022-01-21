using System.ComponentModel.DataAnnotations;

namespace CampSleepaway.Domain.Data
{
    public class CamperNextOfKin
    {
        [Key]
        public int NextOfKinId { get; set; }
        [Key]
        public int CamperId { get; set; }
        [MaxLength(50)]
        public string NextOfKinRelationship { get; set; }
    }
}
