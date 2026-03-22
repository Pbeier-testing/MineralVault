using Microsoft.EntityFrameworkCore;
using MineralCollection.Domain;

namespace MineralCollection.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Mineral> Minerals { get; set; }
    public DbSet<MineralImage> MineralImages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Definieren der Beziehung zwischen Mineral und MineralImage (1:n)
        modelBuilder.Entity<Mineral>()
            .HasMany(m => m.Images)
            .WithOne()
            .HasForeignKey(i => i.MineralId);
    }
}