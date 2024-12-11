using HotelFlow.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelFlow.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
            // Here, the properties are not directly initialized,
            // but the context will handle their initialization.
        }
        public DbSet<Dashboard> Dashboard { get; set; }
        //public DbSet<Users> Users { get; set; } = null!;
        //public DbSet<Car_plate> Car_Plate { get; set; } = null!;
        //public DbSet<Product> Product { get; set; } = null!;
        //public DbSet<Rental> Rental { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dashboard>()
                .HasNoKey(); // ถ้าคุณใช้ Keyless Entity
        }
    }
}
