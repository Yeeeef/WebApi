using Finshark.Data;
using Finshark.DTO;
using Finshark.Helpers;
using Finshark.Interfaces;
using Finshark.Mappers;
using Finshark.Models;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stocks = await _stockRepository.GetAll(query);
            var stocksDTO = stocks.Select(s => s.ToStockDTO()).ToList();
            return Ok(stocksDTO);
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetByID([FromRoute]int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stock = await _stockRepository.GetByID(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDTO());
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDTO stockDTO )
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stockModel =  stockDTO.ToStockFromCreateDTO();
            await _stockRepository.Create(stockModel);
            return Ok(stockModel);
        }

        [HttpPut]
        [Route("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDTO updateDTO)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stock = await _stockRepository.Update(id, updateDTO);
            if (stock == null) return NotFound();
            return Ok(stock.ToStockDTO());
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stock = await _stockRepository.Delete(id);
            if (stock == null) return NotFound();  
            return Ok("Stock Deleted");
        }
    }
};