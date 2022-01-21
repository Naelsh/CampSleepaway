using CampSleepaway.Domain.Data;
using Microsoft.EntityFrameworkCore;

namespace CampSleepaway.Persistence
{
    public class CampSleepawayContext : DbContext
    {
        public DbSet<Cabin> Cabins { get; set; }
        public DbSet<Camper> Campers { get; set; }
        public DbSet<NextOfKin> NextOfKins { get; set; }
        public DbSet<Counselor> Counselors { get; set; }
        public DbSet<CamperNextOfKin> CamperNextOfKins { get; set; }
        public DbSet<CabinCamperStay> CabinCamperStays { get; set; }
        public DbSet<CabinCounselorStay> CabinCounselorStays { get; set; }

        public CampSleepawayContext()
        {

        }

        public CampSleepawayContext(DbContextOptions opt) : base(opt)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #region ConnectionStrings
            // Laptop
            //string connectionString =
            //    @"Data Source=DESKTOP-EJ7V12L\SQLEXPRESS01;" +
            //    @"Initial Catalog = CampSleepaway;" +
            //    @"Integrated Security=true";

            //Stationär
            string connectionString =
                @"Data Source=DESKTOP-JC3MCVE;" +
                @"Initial Catalog = CampSleepaway;" +
                @"Integrated Security=true";

            // Magister
            //string connectionString =
            //    @"Data Source=DESKTOP-T2GL85N\SQLEXPRESS2017;" +
            //    @"Initial Catalog=CS_Niklas_Lindblad;" +
            //    @"Integrated Security=true";
            #endregion

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}
