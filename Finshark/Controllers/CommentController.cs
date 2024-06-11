using System.Runtime.CompilerServices;
using Finshark.Data;
using Finshark.DTO;
using Finshark.Interfaces;
using Finshark.Mappers;
using Finshark.Models;
using Microsoft.AspNetCore.Mvc;

namespace Finshark.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly ICommentRepository commentRepo;
        private readonly IStockRepository stockRepository;
        public CommentController(ApplicationDBContext DBContext, ICommentRepository CommentRepository, IStockRepository StockRepository)
        {
            _dbContext = DBContext;
            commentRepo = CommentRepository;
            stockRepository = StockRepository;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var _comments = await commentRepo.GetAll();
            var commentsDTO = _comments.Select(c => c.ToCommentDTO());
            return Ok(commentsDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID([FromRoute]int id)
        {
            var _comment = await commentRepo.GetById(id);
            if ( _comment== null)
            {
                return NotFound();
            }
            return Ok(_comment);
        }

        [HttpPost("{StockId}")]
        public async Task<IActionResult> Create([FromRoute] int StockId,[FromBody] CreateCommentRequestDTO commentDTO )
        {
            if(!await stockRepository.StockExists(StockId))
            {
                return BadRequest("Stock is not real.");
            }
            Comment commentModel = commentDTO.ToCommentFromCreateDTO(StockId);
            await commentRepo.Create(commentModel);
            return CreatedAtAction(nameof(GetByID), new { id = commentModel}, commentModel);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id , [FromBody] UpdateCommentRequestDTO commentRequestDTO)
        {
            var _comment = await _dbContext.Comments.FindAsync(id);
            if (_comment == null)
            {
                return BadRequest();
            }
            return Ok(await commentRepo.Update(_comment, commentRequestDTO));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var _comment = await commentRepo.Delete(id);
            if (_comment == null)
            {
                return BadRequest("Comment Not Found");
            }
            return Ok("Comment Deleted");
        }
    }
}