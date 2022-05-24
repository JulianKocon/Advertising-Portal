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

          
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
