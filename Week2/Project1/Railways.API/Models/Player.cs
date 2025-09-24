using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace Railways.Models;

public class Player
{
    public int Id { get; set; }
    [Required]
    [MaxLength(16)]
    public string Username { get; set; }
    public int Money { get; set; }
    public List<Stock> Shares { get; set; } = new();
}