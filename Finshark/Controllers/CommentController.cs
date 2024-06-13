using System.Runtime.CompilerServices;
using Finshark.Data;
using Finshark.DTO;
using Finshark.Extensions;
using Finshark.Interfaces;
using Finshark.Mappers;
using Finshark.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Finshark.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository commentRepo;
        private readonly IStockRepository _stockRepo;
        private readonly UserManager<AppUser> _userManager;
        public CommentController(ApplicationDBContext DBContext, ICommentRepository CommentRepository, IStockRepository StockRepository, UserManager<AppUser> userManager)
        {
            commentRepo = CommentRepository;
            _stockRepo = StockRepository;
            _userManager = userManager;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var _comments = await commentRepo.GetAll();
            var _commentsDTO = _comments.Select(c => c.ToCommentDTO());
            return Ok(_commentsDTO);
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
            return Ok(_comment.ToCommentDTO());
        }

        [HttpPost]
        [Authorize]
        [Route("{Symbol:alpha}")]
        public async Task<IActionResult> Create([FromRoute] string Symbol,[FromBody] CreateCommentRequestDTO CommentDTO )
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var stock = await _stockRepo.GetBySymbol(Symbol);
            if(stock ==null) return BadRequest("Stock does not exist");

            var username = User.GetUsername();
            var _appUser = await _userManager.FindByNameAsync(username);
            if (_appUser==null) return BadRequest();

            var _comment = await commentRepo.Create(CommentDTO, stock, _appUser.Id);
            if ( _comment==null) return BadRequest();

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