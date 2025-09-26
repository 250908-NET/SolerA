using Microsoft.EntityFrameworkCore;
using Railways.Models;

namespace Railways.Data;

public class RailwaysDbContext : DbContext
{
    public DbSet<Player> Players { get; set; }
    public DbSet<Stock> Stocks { get; set; }
    public DbSet<Company> Companies { get; set; }

    public RailwaysDbContext(DbContextOptions<RailwaysDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder.Entity<Stock>()
            .HasKey(s => new { s.PlayerId, s.CompanyId });

        modelBuilder.Entity<Stock>()
            .HasOne(s => s.Player)
            .WithMany(p => p.Stocks)
            .HasForeignKey(s => s.PlayerId);

        modelBuilder.Entity<Stock>()
            .HasOne(s => s.Company)
            .WithMany(c => c.Stocks)
            .HasForeignKey(s => s.CompanyId);

        modelBuilder.Entity<Player>().HasData(new Player
        {
            Id = -1,
            Username = "Bank",
            Money = 999999999
        });
    }
}