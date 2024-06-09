using Finshark.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace Finshark.Mappers;

public static class StockMappers
{
    public static StockDTO ToStockDTO(this Stock stock)
    {
        return new StockDTO
        {
            Id = stock.Id,
            Symbol = stock.Symbol,
            CompanyName = stock.CompanyName,
            Industry = stock.Industry,
            Profit =stock.Profit,
            Purchase = stock.Purchase,
            MarketCap = stock.MarketCap
        };
    }

    public static Stock ToStockFromCreateDTO(this CreateStockRequestDTO stockDTO)
    {
        return new Stock
        {
            Symbol = stockDTO.Symbol,
            CompanyName = stockDTO.CompanyName,
            Purchase = stockDTO.Purchase,
            Industry = stockDTO.Industry,
            Profit = stockDTO.Profit,
            MarketCap = stockDTO.MarketCap,
        };
    }
}
