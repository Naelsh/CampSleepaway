using CampSleepaway.Domain.Data;
using CampSleepaway.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CampSleepaway.Application
{
    public class VisitManager : ManagerCore
    {
        private static readonly int _startTime = 10;
        private static readonly int _endTime = 20;
        private static readonly int _maxDuration = 180;

        public VisitManager(CampSleepawayContext context) : base(context)
        {
        }

        public int AddNewVisit(
            int camperId,
            DateTime startTime,
            int durationInMinutes,
            params int[] nextOfKinIds)
        {
            if (IsStartToEarly(startTime)) return 0;
            if (IsEndToLate(startTime, durationInMinutes)) return 0;
            if (IsDurationToLong(durationInMinutes)) return 0;

            CamperManager camperManager = new(_context);
            Camper camper = camperManager.GetById(camperId);
            Visit visit = new Visit()
            {
                StartTime = startTime,
                EndTime = startTime.AddMinutes(durationInMinutes)
            };
            camper.Visits.Add(visit);

            NextOfKinManager nextOfKinManager = new(_context);

            foreach (var nextOfKinId in nextOfKinIds)
            {
                if (!IsNextOfKinResponsibleForCamper(camperId, nextOfKinId)) continue;
                visit.NextOfKins.Add(nextOfKinManager.GetById(nextOfKinId));
            }
            if (visit.NextOfKins.Count == 0) return 0;

            return _context.SaveChanges();
        }

        public List<VisitView> GetVisits(int camperId, int nextOfKinId)
        {
            return (from visit in _context.Visits
                    where visit.CamperId == camperId
                    join nokVisit in _context.NextOfKinVisits on visit.Id equals nokVisit.VisitId
                    where nokVisit.NextOfKinId == nextOfKinId
                    join camper in _context.Campers on visit.CamperId equals camper.Id
                    join camperCabinStay in _context.CabinCamperStays on camper.Id equals camperCabinStay.CamperId
                    join cabin in _context.Cabins on camperCabinStay.CabinId equals cabin.Id
                    join counseloCabinStay in _context.CabinCounselorStays on cabin.Id equals counseloCabinStay.CabinId
                    join counselor in _context.Counselors on counseloCabinStay.CounselorId equals counselor.Id
                    select new VisitView()
                    {
                        CamperName = camper.FirstName + " " + camper.LastName,
                        CabinName = cabin.Name,
                        CounselorName = counselor.FirstName + " " + counselor.LastName,
                        CounselorPhoneNumber = counselor.PhoneNumber,
                        CounselorTitle = counselor.Title,
                        StartTime = visit.StartTime,
                        EndTime = visit.EndTime
                    }).ToList();
        }

        private bool IsNextOfKinResponsibleForCamper(int camperId, int nextOfKinId)
        {
            NextOfKinManager nextOfKinManager = new(_context);
            var campers = nextOfKinManager.GetIdOfAllCampersNextOfKinHasRelationshipWith(nextOfKinId);
            return campers.Find(x => x == camperId) > 0;
        }

        private bool IsDurationToLong(int durationInMinutes)
        {
            return durationInMinutes > _maxDuration;
        }

        private bool IsEndToLate(DateTime startTime, int durationInMinutes)
        {
            return startTime.AddMinutes(durationInMinutes).Hour > _endTime;
        }

        private bool IsStartToEarly(DateTime startTime)
        {
            return startTime.Hour < _startTime;
        }

    }
}

namespace CampSleepaway.Application
{
    public class VisitView
    {
        public string CamperName { get; set; }
        public string CabinName { get; set; }
        public string CounselorName { get; set; }
        public string CounselorPhoneNumber { get; set; }
        public string CounselorTitle { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
