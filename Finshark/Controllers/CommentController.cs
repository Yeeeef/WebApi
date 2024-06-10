using Finshark.Data;
using Finshark.DTO;
using Finshark.Models;
using Microsoft.AspNetCore.Mvc;

namespace Finshark.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ApplicationDBContext _dbContext;
        public CommentController(ApplicationDBContext context)
        {
            _dbContext = context;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var _comments = _dbContext.Comments.ToList();
            return Ok(_comments);
        }

        [HttpGet("{id}")]
        public IActionResult GetByID([FromRoute]int id)
        {
            var _comment = _dbContext.Comments.Find(id);
            if ( _comment== null)
            {
                return NotFound();
            }
            return Ok(_comment);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateCommentRequestDTO commentDTO )
        {
            var commentModel = commentDTO.ToCommentFromCreateDTO();
            _dbContext.Comments.Add(commentModel);
            _dbContext.SaveChanges();
            return CreatedAtAction(nameof(GetByID), new {id = commentModel.Id}, commentModel.ToCommentDTO());


        }
    }
}