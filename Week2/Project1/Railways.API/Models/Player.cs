

namespace Railways.Models;

public class Player
{
    public int Id { get; set; }
    public string Username { get; set; }
    public int Money { get; set; }
    public List<Stocks> Shares { get; set; } = new();
}