using Finshark.Data;
using Finshark.DTO;
using Finshark.Helpers;
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


    public async Task<List<Stock>> GetAll(QueryObject query)
    {
        var stocks = _dbContext.Stocks.Include(c => c.Comments).AsQueryable();

        if(!string.IsNullOrEmpty(query.CompanyName))
        {
            stocks = stocks.Where(s => s.CompanyName.Contains(query.CompanyName));
        }

        if(!string.IsNullOrEmpty(query.Symbol))
        {
            stocks = stocks.Where(s => s.Symbol.Contains(query.Symbol));
        }

        return await stocks.ToListAsync();
    }

    public async Task<Stock?> GetByID(int id) => await _dbContext.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(i => i.Id == id);


        public async Task<Stock> Create(Stock stockModel)
    {
        await _dbContext.Stocks.AddAsync(stockModel);
        await _dbContext.SaveChangesAsync();
        return stockModel;
        
    }

    public async Task<Stock?> Delete(int id)
    {
        var stock = await _dbContext.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(s => s.Id == id);
            if(stock == null)
            {
                return null;
            }
        _dbContext.Stocks.Remove(stock);
        await _dbContext.SaveChangesAsync();
        return stock;
    }

    public async Task<Stock?> Update(int id, UpdateStockRequestDTO stockModel)
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

    public async Task<bool> StockExists(int id)
    {
        return await _dbContext.Stocks.AnyAsync(s => s.Id == id);
    }
}
