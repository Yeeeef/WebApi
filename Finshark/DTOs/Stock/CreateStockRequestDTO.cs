namespace Finshark;

public class CreateStockRequestDTO
{
    public string Symbol { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public decimal Purchase { get; set; }
    public int Profit { get; set; }
    public string Industry { get; set; } = string.Empty;
    public long MarketCap { get; set; }
}
