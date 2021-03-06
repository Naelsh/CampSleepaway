using System.ComponentModel.DataAnnotations;

namespace CampSleepaway.Domain.Data
{
    public class CamperNextOfKin
    {
        public int NextOfKinId { get; set; }
        public int CamperId { get; set; }
        [MaxLength(50)]
        public string NextOfKinRelationship { get; set; }
    }
}
