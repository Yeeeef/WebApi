﻿using System.ComponentModel.DataAnnotations;
using Finshark.Models;

namespace Finshark.DTO;

public class CreateStockRequestDTO
{
    [Required]
    [MaxLength(6, ErrorMessage = "Symbol cannot be longer than 6 characters")]
    public string Symbol { get; set; } = string.Empty;
    [Required]
    [MaxLength(15, ErrorMessage = "CompanyName cannot be longer than 15 characters")]
    public string CompanyName { get; set; } = string.Empty;
    [Required]
    [Range(-5,1000000)]
    public decimal Purchase { get; set; }
    [Required]
    [Range(-10000, 1000)]
    public int Profit { get; set; }
    [Required]
    [MaxLength(15, ErrorMessage = "Industry cannot be longer than 15 characters")]
    public string Industry { get; set; } = string.Empty;
    [Range(1,10000000000)]
    public long MarketCap { get; set; }
}
