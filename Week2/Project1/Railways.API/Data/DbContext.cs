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
            .HasKey(s => s.Id); // PK is Stock.Id

        modelBuilder.Entity<Stock>()
            .HasOne(s => s.Player)
            .WithMany(p => p.Stocks)
            .HasForeignKey(s => s.PlayerId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Stock>()
            .HasOne(s => s.Company)
            .WithMany(c => c.Stocks)
            .HasForeignKey(s => s.CompanyId)
            .IsRequired(true);

        modelBuilder.Entity<Player>().HasData(
            new Player { Id = -1, Username = "Bank", Money = 0 }
        );

        modelBuilder.Entity<Company>().HasData(
            new Company { Id = 1, Name = "Expansive", Money = 0, TotalShares = 5, StockPriceIndex = 0 },
            new Company { Id = 2, Name = "Express", Money = 0, TotalShares = 5, StockPriceIndex = 0 },
            new Company { Id = 3, Name = "Suburban", Money = 0, TotalShares = 5, StockPriceIndex = 0 },
            new Company { Id = 4, Name = "Resourceful", Money = 0, TotalShares = 5, StockPriceIndex = 0 },
            new Company { Id = 5, Name = "Eastern Mining", Money = 0, TotalShares = 5, StockPriceIndex = 0 },
            new Company { Id = 6, Name = "Spacious", Money = 0, TotalShares = 5, StockPriceIndex = 0 },
            new Company { Id = 7, Name = "Agricultural", Money = 0, TotalShares = 5, StockPriceIndex = 0 },
            new Company { Id = 8, Name = "Bridging", Money = 0, TotalShares = 5, StockPriceIndex = 0 },
            new Company { Id = 9, Name = "Northern Port", Money = 0, TotalShares = 5, StockPriceIndex = 0 },
            new Company { Id = 10, Name = "Adaptive", Money = 0, TotalShares = 5, StockPriceIndex = 0 },
            new Company { Id = 11, Name = "Overnight", Money = 0, TotalShares = 5, StockPriceIndex = 0 },
            new Company { Id = 12, Name = "Tunneling", Money = 0, TotalShares = 5, StockPriceIndex = 0 },
            new Company { Id = 13, Name = "Manufacturing", Money = 0, TotalShares = 5, StockPriceIndex = 0 },
            new Company { Id = 14, Name = "Twin Cities", Money = 0, TotalShares = 5, StockPriceIndex = 0 },
            new Company { Id = 15, Name = "Circus", Money = 0, TotalShares = 5, StockPriceIndex = 0 },
            new Company { Id = 16, Name = "Coupling", Money = 0, TotalShares = 5, StockPriceIndex = 0 }
        );
        
    }
}