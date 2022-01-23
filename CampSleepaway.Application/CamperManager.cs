using CampSleepaway.Domain;
using CampSleepaway.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CampSleepaway.Application
{
    public class CamperManager : ManagerCore
    {
        public CamperManager(CampSleepawayContext context) : base(context)
        { }

        public int AddCamper(Camper newCamper)
        {
            if (newCamper.FirstName == null) { return 0; }
            if (newCamper.LastName == null) { return 0; }
            _context.Campers.Add(newCamper);
            var result = _context.SaveChanges();
            return result;
        }

        public List<Camper> GetCampersByName(string firstName)
        {
            return _context.Campers.Where(x => x.FirstName == firstName).ToList();
        }

        public List<Camper> GetAllItems()
        {
            return _context.Campers.ToList();
        }

        public Camper GetById(int id)
        {
            return _context.Campers.FirstOrDefault(c => c.Id == id);
        }

        public List<CamperCabinView> GetAllCamperAndNextOfKinRelationsOrderedByCabin(DateTime date)
        {
            CamperManager camperManager = new(_context);

            var campers = camperManager.GetAllItems();

            List<CamperCabinView> camperViews = new List<CamperCabinView>();

            foreach (var camper in campers)
            {
                Cabin activeCabin = camperManager.GetActiveCabin(camper.Id, date);
                List<NextOfKinView> nextOfKins = camperManager.GetAllNextOfKinsToCamperById(camper.Id);
                camperViews.Add(new CamperCabinView()
                {
                    NextOfKins = nextOfKins,
                    CamperId = camper.Id,
                    CamperFirstName = camper.FirstName,
                    CamperLastName = camper.LastName,
                    CabinId = activeCabin == null ? 0 : activeCabin.Id,
                    CabinName = activeCabin == null ? string.Empty : activeCabin.Name
                });
            }

            return camperViews;
        }

        public int ChangeNameOfCamper(int camperIdToChange, string newFirstName, string newLastName)
        {
            Camper camper = _context.Campers.FirstOrDefault(x => x.Id == camperIdToChange);
            if (camper == null) { return 0; }
            camper.FirstName = newFirstName;
            camper.LastName = newLastName;
            return _context.SaveChanges();
        }

        private List<NextOfKinView> GetAllNextOfKinsToCamperById(int id)
        {
            return (from nextOfKin in _context.NextOfKins
                    join noks in _context.CamperNextOfKins on nextOfKin.Id equals noks.NextOfKinId
                    where noks.CamperId == id
                    select new NextOfKinView
                    {
                        NextOfKinId = nextOfKin.Id,
                        FirstName = nextOfKin.FirstName,
                        LastName = nextOfKin.LastName,
                        Relationship = noks.NextOfKinRelationship
                    }).ToList();
        }

        private Cabin GetActiveCabin(int id, DateTime date)
        {
            return (from cabin in _context.Cabins
                    join ccs in _context.CabinCamperStays on cabin.Id equals ccs.CabinId
                    where ccs.CamperId == id && ccs.StartTime <= date && date <= ccs.EndTime
                    select cabin).FirstOrDefault();
        }
    }
}

namespace CampSleepaway.Application
{
    public class CamperCabinView
    {
        public int CamperId { get; set; }
        public string CamperFirstName { get; set; }
        public string CamperLastName { get; set; }
        public int CabinId { get; set; }
        public string CabinName { get; set; }
        public List<NextOfKinView> NextOfKins { get; set; }
    }

    public class NextOfKinView
    {
        public int NextOfKinId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Relationship { get; set; }
    }
}
