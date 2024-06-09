using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Finshark.Models;

public class Stock
{
    public string Url { get; set; } = string.Empty;
    public int Id { get; set; }
    public string Symbol { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    [Column(TypeName = "decimal(18,2)")]
    public decimal Purchase { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Profit { get; set; }
    public string Industry { get; set; } = string.Empty;
    public long MarketCap { get; set; }

    //links Comment(s) to Stock
    public List<Comment> Comments { get; set; } = new List<Comment>();
}
