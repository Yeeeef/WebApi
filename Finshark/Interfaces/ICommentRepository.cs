using Finshark.DTO;
using Finshark.Helpers;
using Finshark.Models;

namespace Finshark.Interfaces;

public interface ICommentRepository
{
    //Create
    Task<Comment?> Create(CreateCommentRequestDTO commentDTO, int StockId);

    //Read
    Task<Comment?> GetById(int id);
    Task<List<Comment>> GetAll();

    //Update
    Task<CommentDTO?> Update(int id, UpdateCommentRequestDTO update);

    //Delete
    Task<Comment?> Delete(int id);
}
