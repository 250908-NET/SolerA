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
    }
}