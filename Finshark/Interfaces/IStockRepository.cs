using Finshark.DTO;
using Finshark.Models;

namespace Finshark.Interfaces;

public interface IStockRepository
{
    public Task<Stock?> GetByIDAsync(int id);
    public Task<List<Stock>> GetAllAsync();
    public Task<Stock> CreateAsync(Stock stock);
    public Task<Stock?> DeleteAsync(int id);
    public Task <Stock?> UpdateAsync(int id, UpdateStockRequestDTO stockRequestDTO);
}
