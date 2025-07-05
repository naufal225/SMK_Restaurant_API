using Microsoft.EntityFrameworkCore;
using SMK_Restaurant_API.Models;

namespace SMK_Restaurant_API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Msmember> Msmember { get; set; }
        public DbSet<Msmenu> Msmenu { get; set; }

        public DbSet<Headerorder> Headerorder { get; set; }

        public DbSet<Review> Review { get; set; }
        public DbSet<PartyPackage> PartyPackage { get; set; }
        public DbSet<PartyPackageDetail> PartyPackageDetail { get; set; }
        public DbSet<OrderPackage> OrderPackage { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Member)
                .WithMany(m => m.Reviews)
                .HasForeignKey(r => r.MemberID)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }

}
