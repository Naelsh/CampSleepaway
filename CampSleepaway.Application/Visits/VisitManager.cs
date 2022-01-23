using CampSleepaway.Application.Campers;
using CampSleepaway.Application.NextOfKins;
using CampSleepaway.Domain.Data;
using CampSleepaway.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampSleepaway.Application.Visits
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
