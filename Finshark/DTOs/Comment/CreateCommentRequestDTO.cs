using System.Text;
using Finshark.Models;

namespace Finshark.DTO;

public class CreateCommentRequestDTO
{
    public string Subject { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreationTime { get; set; }
    public int? StockId { get; set; }
}
