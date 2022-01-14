using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampSleepaway.Domain
{
    public class CamperNextOfKin
    {
        public int NextOfKinId { get; set; }
        public int CamperId { get; set; }
        public string NextOfKinRelationship { get; set; }
    }
}
