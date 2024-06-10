using Finshark.Data;
using Finshark.DTO;
using Finshark.Mappers;
using Finshark.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace Finshark.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _dbContext;
        public StockController(ApplicationDBContext context)
        {
            _dbContext = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stocks = await _dbContext.Stocks.ToListAsync();
            var stockDTO = stocks.Select(s => s.ToStockDTO());
            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID([FromRoute]int id)
        {
            var stock = await _dbContext.Stocks.FindAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDTO stockDTO )
        {
            var stockModel =  stockDTO.ToStockFromCreateDTO();
            await _dbContext.Stocks.AddAsync(stockModel);
            _dbContext.SaveChanges();
            return CreatedAtAction(nameof(GetByID), new {id = stockModel.Id}, stockModel.ToStockDTO());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDTO updateDTO)
        {
            var stock = await _dbContext.Stocks.FirstOrDefaultAsync(s => s.Id == id);
            if (stock == null)
            {
                return NotFound();
            }
            stock.Purchase = updateDTO.Purchase;
            stock.MarketCap = updateDTO.MarketCap;
            stock.Profit = updateDTO.Profit;
            stock.Symbol = updateDTO.Symbol;
            stock.CompanyName = updateDTO.CompanyName;
            stock.Industry = updateDTO.Industry;

            await _dbContext.SaveChangesAsync();

            return Ok(stock.ToStockDTO());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var stock = await _dbContext.Stocks.FirstOrDefaultAsync(s => s.Id == id);
            if(stock == null)
            {
                return NotFound();
            }

            _dbContext.Stocks.Remove(stock);

            await _dbContext.SaveChangesAsync();

            return NoContent();
            
        }
    }
};