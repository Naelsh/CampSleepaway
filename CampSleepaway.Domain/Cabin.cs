using System.Collections.Generic;

namespace CampSleepaway.Domain
{
    public class Cabin
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Person> Residents { get; set; } = new List<Person>();
    }
}
