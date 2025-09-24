namespace Railways.Models;

public class Company
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Money { get; set; }
    public int StockValue{ get; set; }
    public List<Stocks> Shares { get; set; } = new();
    public int? PresidentID { get; set; }
    public Player? President { get; set; }
}