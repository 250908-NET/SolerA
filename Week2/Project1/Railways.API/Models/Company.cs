using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Railways.Models;

public class Company
{
    public int Id { get; set; }
    [Required]
    [MaxLength(30)]
    public string Name { get; set; }
    public int Money { get; set; }
    public int StockValue { get; set; }
    public List<Stock> Shares { get; set; } = new();
    public int? PresidentID { get; set; }
    public Player? President { get; set; }
}