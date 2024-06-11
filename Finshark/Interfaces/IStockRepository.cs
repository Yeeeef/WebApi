﻿using Finshark.DTO;
using Finshark.Models;

namespace Finshark.Interfaces;

public interface IStockRepository
{
    public Task<Stock?> GetByID(int id);
    public Task<List<Stock>> GetAll();
    public Task<Stock> Create(Stock stock);
    public Task<Stock?> Delete(int id);
    public Task <Stock?> Update(int id, UpdateStockRequestDTO stockModel);
    public Task <bool> StockExists (int id);
}
