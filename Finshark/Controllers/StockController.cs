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
        public async Task<IActionResult> GetAll()
        {
            var stocks = await _stockRepository.GetAll();
            var stocksDTO = stocks.Select(s => s.ToStockDTO());
            return Ok(stocksDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID([FromRoute]int id)
        {
            var stock = await _stockRepository.GetByID(id);
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
            await _stockRepository.Create(stockModel);
            return CreatedAtAction(nameof(GetByID), new { id = stockModel}, stockModel);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDTO updateDTO)
        {
            var stock = await _stockRepository.Update(id, updateDTO);
            if (stock == null) return NotFound();
            return Ok(stock.ToStockDTO());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var stock = await _stockRepository.Delete(id);
            if (stock == null) return NotFound();  
            return NoContent();
        }
    }
};