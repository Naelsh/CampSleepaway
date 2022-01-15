using CampSleepaway.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace Backend
{
    public class CampSleepawayContext : DbContext
    {
        public DbSet<Cabin> Cabins { get; set; }
        public DbSet<Camper> Campers { get; set; }
        public DbSet<NextOfKin> NextOfKins { get; set; }
        public DbSet<Counselor> Counselors { get; set; }

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SetupCamperCabin(modelBuilder);
            SetupCounselorCabin(modelBuilder);
            SetupVisits(modelBuilder);
            SetupCamperNextOfKin(modelBuilder);
        }

        private void SetupCamperNextOfKin(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Camper>()
                            .HasMany(camper => camper.NextOfKins)
                            .WithMany(nextOfKin => nextOfKin.Children)
                            .UsingEntity<CamperNextOfKin>
                            (v => v.HasOne<NextOfKin>().WithMany(),
                            v => v.HasOne<Camper>().WithMany());
        }

        private void SetupVisits(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Camper>()
                            .HasMany(camper => camper.NextOfKins)
                            .WithMany(nextOfKin => nextOfKin.Children)
                            .UsingEntity<Visit>
                            (v => v.HasOne<NextOfKin>().WithMany(),
                            v => v.HasOne<Camper>().WithMany());
        }

        private void SetupCounselorCabin(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cabin>()
                            .HasMany(cabin => cabin.Counselors)
                            .WithMany(counselor => counselor.CabinStays)
                            .UsingEntity<CabinCounselorStay>
                            (ccs => ccs.HasOne<Counselor>().WithMany(),
                            ccs => ccs.HasOne<Cabin>().WithMany());
        }

        private void SetupCamperCabin(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cabin>()
                         .HasMany(cabin => cabin.Campers)
                         .WithMany(camper => camper.CabinStays)
                         .UsingEntity<CabinCamperStay>
                          (ccs => ccs.HasOne<Camper>().WithMany(),
                           ccs => ccs.HasOne<Cabin>().WithMany());
        }
    }
}
