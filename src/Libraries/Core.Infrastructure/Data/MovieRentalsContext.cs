using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructure.Data;

public class MovieRentalsContext(DbContextOptions<MovieRentalsContext> options) : DbContext(options)
{
    public DbSet<Rental> Rentals => Set<Rental>();

    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.Entity<Rental>(e =>
        {
            e.ToTable("RENTALS");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).ValueGeneratedOnAdd();
            e.Property(x => x.CustomerName).HasMaxLength(200).IsRequired();
            e.Property(x => x.MovieId).IsRequired();
            e.Property(x => x.RentedAt).IsRequired();
            e.Property(x => x.DueAt).IsRequired();
        });
    }
}
