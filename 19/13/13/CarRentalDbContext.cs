using Microsoft.EntityFrameworkCore;

namespace CarRental
{
    public class CarRentalDbContext : DbContext
    {
        public CarRentalDbContext(DbContextOptions<CarRentalDbContext> options) : base(options)
        {
        }

        public DbSet<CarEntity> Cars => Set<CarEntity>();
        public DbSet<RentalEntity> Rentals => Set<RentalEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarEntity>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.Make).HasMaxLength(80).IsRequired();
                b.Property(x => x.Model).HasMaxLength(120).IsRequired();
                b.Property(x => x.PricePerDay).IsRequired();
                b.Property(x => x.FleetSize).IsRequired();

                b.HasIndex(x => new { x.Make, x.Model }).IsUnique();
            });

            modelBuilder.Entity<RentalEntity>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.ClientName).HasMaxLength(120).IsRequired();
                b.Property(x => x.StartDate).IsRequired();
                b.Property(x => x.EndDate).IsRequired();
                b.Property(x => x.PricePerDay).IsRequired();

                b.HasOne(x => x.Car)
                    .WithMany()
                    .HasForeignKey(x => x.CarId)
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasIndex(x => x.CarId);
            });
        }
    }
}

