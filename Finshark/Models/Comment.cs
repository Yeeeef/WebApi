namespace Finshark.Models;

public class Comment
{
    public int Id { get; set; }
    public string Url { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreationTime { get; set; } = DateTime.Now;
    //links Comment to Stock
    public int? StockId { get; set; }
    //Navigation Property
    public Stock? Stock { get; set; }

}
