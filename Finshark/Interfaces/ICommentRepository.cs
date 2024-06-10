using Finshark.Models;

namespace Finshark.Interfaces;

public interface ICommentRepository
{
    Task<List<Comment>> GetAllAsync();
    Task<Comment> CreateAsync(Comment comment);
    Task<Comment?> GetByIdAsync (int id);
}
