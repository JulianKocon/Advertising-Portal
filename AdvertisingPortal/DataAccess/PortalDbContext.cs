using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvertisingPortal.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdvertisingPortal.DataAccess
{
    public class PortalDbContext :DbContext
    {
        public PortalDbContext()
        {

        }

        public PortalDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PurchaseOrder>()
                .HasKey(c => new { c.IdUser, c.IdAdvertisement });

            modelBuilder.Entity<Region>(a =>
            {
                a.HasData(
                    new Region { IdRegion = 1, Name = "Warsaw, Poland" }
                    );

                a.HasData(
                    new Region { IdRegion = 2, Name = "Gdynia, Poland" }
                    );

                a.HasData(
                    new Region { IdRegion = 3, Name = "New York, USA" }
                    );

                a.HasData(
                    new Region { IdRegion = 4, Name = "Dallas, USA" }
                    );

            });

            modelBuilder.Entity<Category>(a =>
            {
                

                a.HasData(
                    new Category { IdCategory = 1, Name = "Vehicles" }
                    );

                a.HasData(
                    new Category { IdCategory = 2, Name = "Real estate" }
                    );

                a.HasData(
                   new Category { IdCategory = 3, Name = "House and garden" }
                   );

                a.HasData(
                   new Category { IdCategory = 4, Name = "Pets" }
                   );
            });

            modelBuilder.Entity<Advertisement>(a =>
            {
                a.HasData(
                    new Advertisement
                    {
                        IdAdvertisement = 1,
                        IdUser = 1,
                        Name = "Toyota mr2",
                        Description = "Old but gold",
                        Date = DateTime.Parse("18/05/2022"),
                        IdRegion = 1,
                        Price = 24000
                    }
                    );

                a.HasData(
                   new Advertisement
                   {
                       IdAdvertisement = 2,
                       IdUser = 1,
                       Name = "Pets",
                       Description = "White Maine Coon for sale",
                       Date = DateTime.Parse("18/05/2022"),
                       IdRegion = 1,
                       Price = 1000
                   }
                   );
            });

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
