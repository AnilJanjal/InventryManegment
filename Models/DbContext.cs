using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Controllers;
using WebApplication3.Models;

namespace YourApp.Models
{
    public class IDbContext : DbContext
    {
        public IDbContext(DbContextOptions<IDbContext> options) : base(options) { }

        public DbSet<WebApplication3.Models.Items> Items { get; set; }  // This will map to the Products table
        public DbSet<Locations> Locations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<WebApplication3.Models.Items>()
                .HasKey(i => i.ItemId); // Explicitly define the primary key
            modelBuilder.Entity<WebApplication3.Models.Locations>()
               .HasKey(i => i.LocationId);
        }
    }
}
