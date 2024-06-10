using Finshark.Data;
using Finshark.DTO;
using Finshark.Interfaces;
using Finshark.Models;
using Microsoft.EntityFrameworkCore;

namespace Finshark.Repository;

public class StockRepository : IStockRepository
{
    private readonly ApplicationDBContext _dbContext;
    public StockRepository(ApplicationDBContext DBContext)
    {
        _dbContext = DBContext;
    }


    public Task<List<Stock>> GetAllAsync()
    {
        return _dbContext.Stocks.ToListAsync();
    }

    public async Task<Stock?> GetByIDAsync(int id) => await _dbContext.Stocks.FirstOrDefaultAsync(i => i.Id == id);


        public async Task<Stock> CreateAsync(Stock stockModel)
    {
        await _dbContext.Stocks.AddAsync(stockModel);
        await _dbContext.SaveChangesAsync();
        return stockModel;
        
    }

    public async Task<Stock?> DeleteAsync(int id)
    {
        var stock = await _dbContext.Stocks.FirstOrDefaultAsync(s => s.Id == id);
            if(stock == null)
            {
                return null;
            }
        _dbContext.Stocks.Remove(stock);
        await _dbContext.SaveChangesAsync();
        return stock;
    }

    public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDTO stockModel)
            {
            var stock = await _dbContext.Stocks.FirstOrDefaultAsync(s => s.Id == id);
            if (stock == null)
            {
                return null;
            }
            stock.Purchase = stockModel.Purchase;
            stock.MarketCap = stockModel.MarketCap;
            stock.Profit = stockModel.Profit;
            stock.Symbol = stockModel.Symbol;
            stock.CompanyName = stockModel.CompanyName;
            stock.Industry = stockModel.Industry;

            await _dbContext.SaveChangesAsync();

            return stock;
        }
}
