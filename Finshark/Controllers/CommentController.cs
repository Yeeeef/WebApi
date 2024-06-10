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
        public CommentController(ApplicationDBContext context, ICommentRepository commentRepository)
        {
            _dbContext = context;
            commentRepo = commentRepository;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var _comments = await commentRepo.GetAllAsync();
            var commentsDTO = _comments.Select(c => c.ToCommentDTO());
            return Ok(commentsDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID([FromRoute]int id)
        {
            var _comment = await commentRepo.GetByIdAsync(id);
            if ( _comment== null)
            {
                return NotFound();
            }
            return Ok(_comment);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCommentRequestDTO commentDTO )
        {
            Comment commentModel = commentDTO.ToCommentFromCreateDTO();
            await commentRepo.CreateAsync(commentModel);
            return Ok(commentModel);

        }
    }
}