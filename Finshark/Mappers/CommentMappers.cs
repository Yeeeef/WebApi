using System.Reflection.Metadata.Ecma335;
using Finshark.Models;
using Finshark.DTO;
using Microsoft.AspNetCore.Mvc;
using Finshark.Data;
using Finshark.Controllers;

namespace Finshark.Mappers
{
    [Route("api/comment")]
    [ApiController]
    public static class CommentMapper
    {


        public static CommentDTO ToCommentDTO(this Comment comment)
        {
            return new CommentDTO
            {
                Id = comment.Id,
                Subject = comment.Subject,
                Content = comment.Content,
            };
        }



        public static Comment ToCommentFromCreateDTO(this CreateCommentRequestDTO CommentDTO, int Id)
        {
            return new Comment
            {
                Subject = CommentDTO.Subject,
                Content = CommentDTO.Content,
                StockId = Id
            };
        }
    }
};