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
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var _comments = await commentRepo.GetAll();
            var commentsDTO = _comments.Select(c => c.ToCommentDTO());
            return Ok(commentsDTO);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByID([FromRoute]int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var _comment = await commentRepo.GetById(id);
            if ( _comment== null)
            {
                return NotFound();
            }
            return Ok(_comment);
        }

        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> Create([FromRoute] int StockId,[FromBody] CreateCommentRequestDTO commentDTO )
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var _comment = await commentRepo.Create(commentDTO, StockId);
            if ( _comment== null)
            {
                return NotFound();
            }
            return Ok(_comment.ToCommentDTO());

        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id , [FromBody] UpdateCommentRequestDTO commentRequestDTO)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var commentDTO = await commentRepo.Update(id,commentRequestDTO);
            if (commentDTO == null)
            {
                return BadRequest();
            }
            return Ok(commentDTO);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var _comment = await commentRepo.Delete(id);
            if (_comment == null)
            {
                return BadRequest("Comment Not Found");
            }
            return Ok("Comment Deleted");
        }
    }
}