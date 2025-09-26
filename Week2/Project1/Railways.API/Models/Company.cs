using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Railways.Models;

public class Company
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Money { get; set; }
    public int TotalShares { get; set; }
    public int StockPriceIndex { get; set; }
    public int TreasuryShares => TotalShares - Stocks.Sum(s => s.SharesOwned);
    public List<Stock> Stocks { get; set; } = new();

    public int? PresidentId { get; set; }
    public Player? President { get; set; }
}