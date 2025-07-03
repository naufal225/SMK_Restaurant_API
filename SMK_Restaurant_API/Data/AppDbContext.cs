using Microsoft.EntityFrameworkCore;
using SMK_Restaurant_API.Models;

namespace SMK_Restaurant_API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Msmember> Msmembers { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<PartyPackage> PartyPackages { get; set; }
        public DbSet<PartyPackageDetail> PartyPackageDetails { get; set; }
        public DbSet<OrderPackage> OrderPackages { get; set; }
    }

}
