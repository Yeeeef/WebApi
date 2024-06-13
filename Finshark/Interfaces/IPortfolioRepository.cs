using Finshark.Models;

namespace Finshark.Interfaces;

public interface IPortfolioRepository
{
    Task<List<Stock>> GetUserPortfolio(AppUser user);

    Task<Portfolio> CreateUserPortfolio(AppUser user, Stock stock);
    Task<Portfolio?> DeletePortfolio(AppUser user, string symbol);
}
