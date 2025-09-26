using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Railways.Models;

public class Stock
{
    public int Id{ get; set; }
    public int PlayerId { get; set; }
    public Player Player { get; set; }

    public int CompanyId { get; set; }
    public Company Company { get; set; }
    
    public int SharesOwned { get; set; }
}