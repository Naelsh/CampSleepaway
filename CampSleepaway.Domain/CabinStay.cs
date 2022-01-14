using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampSleepaway.Domain
{
    public class CabinStay
    {
        public int PersonId { get; set; }
        public int CabinId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
