using Finshark.Data;
using Finshark.Interfaces;
using Finshark.Migrations;
using Finshark.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Finshark.Repository;

public class PortfolioRepository : IPortfolioRepository
{
    private readonly ApplicationDBContext _dbContext;
    private readonly IStockRepository _stockRepo;
    public PortfolioRepository(ApplicationDBContext DBContext, IStockRepository StockRepo)
    {
        _dbContext = DBContext;
        _stockRepo = StockRepo;
    }

    public async Task<Portfolio> CreateUserPortfolio(AppUser user, Stock stock)
    {
        Portfolio _portfolio = new Portfolio{AppUserId = user.Id, AppUser = user, StockId = stock.Id, Stock = stock};
        _dbContext.Portfolios.Add(_portfolio);
        await _dbContext.SaveChangesAsync();
        return _portfolio;
    }

    public async Task<List<Stock>> GetUserPortfolio(AppUser user)
    {
        return await _dbContext.Portfolios.Where(u => u.AppUserId == user.Id)
        .Select(stock => new Stock {
            Id = stock.StockId,
            CompanyName = stock.Stock.CompanyName,
            Symbol = stock.Stock.Symbol,
            Purchase = stock.Stock.Purchase,
            Profit = stock.Stock.Profit,
            Industry = stock.Stock.Industry,
            MarketCap = stock.Stock.MarketCap,
        } ).ToListAsync();
    }

    public async Task<Portfolio?> DeletePortfolio(AppUser User, string Symbol)
    {
        var portfolio = await _dbContext.Portfolios.FirstOrDefaultAsync(x => x.AppUserId == User.Id && x.Stock.Symbol == Symbol);
        if(portfolio == null) return null;

        _dbContext.Portfolios.Remove(portfolio);
        await _dbContext.SaveChangesAsync();
        return portfolio;
    }
}
