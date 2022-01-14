using System.Collections.Generic;

namespace CampSleepaway.Domain
{
    public class NextOfKin : Person
    {
        public string PhoneNumber { get; set; }
        public string MailAddress { get; set; }
        public List<Camper> Children { get; set; } = new List<Camper>();
        public List<Visit> Visits { get; set; } = new List<Visit>();
    }
}
