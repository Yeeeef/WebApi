﻿using System.Security.Cryptography.X509Certificates;
using Finshark.Models;

namespace Finshark.DTO;

public class CommentDTO
{
    public int Id { get; set; }
    public string Subject { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreationTime { get; set;} = DateTime.Now;
    public int? StockId { get; set; }
}