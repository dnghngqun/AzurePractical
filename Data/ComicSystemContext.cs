using Microsoft.EntityFrameworkCore;
using ComicSystem.Models;

namespace ComicSystem.Data
{
    public class ComicSystemContext : DbContext
    {
        public ComicSystemContext(DbContextOptions<ComicSystemContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<ComicBook> ComicBooks { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<RentalDetail> RentalDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Thiết lập quan hệ 1-N giữa Customer và Rentals
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Rentals)
                .WithOne(r => r.Customer)
                .HasForeignKey(r => r.CustomerID)
                .OnDelete(DeleteBehavior.Cascade);

            // Thiết lập quan hệ 1-N giữa Rental và RentalDetails
            modelBuilder.Entity<Rental>()
                .HasMany(r => r.RentalDetails)
                .WithOne(rd => rd.Rental)
                .HasForeignKey(rd => rd.RentalID)
                .OnDelete(DeleteBehavior.Cascade);

            // Thiết lập quan hệ 1-N giữa ComicBook và RentalDetails
            modelBuilder.Entity<ComicBook>()
                .HasMany(cb => cb.RentalDetails)
                .WithOne(rd => rd.ComicBook)
                .HasForeignKey(rd => rd.ComicBookID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}