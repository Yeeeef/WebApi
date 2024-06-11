using Finshark.DTO;
using Finshark.Models;

namespace Finshark.Interfaces;

public interface ICommentRepository
{
    Task<List<Comment>> GetAll();
    Task<Comment> Create(Comment comment);
    Task<Comment?> GetById(int id);
    Task<Comment> Update(Comment comment, UpdateCommentRequestDTO update);
}
