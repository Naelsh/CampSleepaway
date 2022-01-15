using System.Collections.Generic;

namespace CampSleepaway.Domain
{
    public class NextOfKin
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string MailAddress { get; set; }
        public List<Camper> Children { get; set; } = new List<Camper>();
    }
}
