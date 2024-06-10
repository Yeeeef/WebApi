using Finshark.Data;
using Finshark.DTO;
using Finshark.Interfaces;
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
        private readonly IStockRepository _stockRepository;
        public StockController(ApplicationDBContext context, IStockRepository repository)
        {
            _dbContext = context;
            _stockRepository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var stocks = await _stockRepository.GetAllAsync();
            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIDAsync([FromRoute]int id)
        {
            var stock = await _stockRepository.GetByIDAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDTO());
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateStockRequestDTO stockDTO )
        {
            var stockModel =  stockDTO.ToStockFromCreateDTO();
            await _stockRepository.CreateAsync(stockModel);
            return Ok(stockModel.ToStockDTO());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] UpdateStockRequestDTO updateDTO)
        {
            var stock = await _stockRepository.UpdateAsync(id, updateDTO);
            if (stock == null) return NotFound();
            return Ok(stock.ToStockDTO());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var stock = await _stockRepository.DeleteAsync(id);
            if (stock == null) return NotFound();  
            return NoContent();
        }
    }
};