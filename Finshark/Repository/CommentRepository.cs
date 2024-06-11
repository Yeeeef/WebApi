using Finshark.Data;
using Finshark.DTO;
using Finshark.Helpers;
using Finshark.Interfaces;
using Finshark.Mappers;
using Finshark.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging.Abstractions;

namespace Finshark;

public class CommentRepository : ICommentRepository
{
    
    private readonly ApplicationDBContext _dbContext;
    public CommentRepository(ApplicationDBContext DBContext)
    {
        _dbContext = DBContext;
    }
    
    public async Task<List<Comment>> GetAll()
    { 
        var _comments = _dbContext.Comments.Include(s => s.Stock);
        return  await _comments.ToListAsync();
    }

    public async Task<Comment?> Create(CreateCommentRequestDTO commentDTO, int StockId)
    {
        if (_dbContext.Stocks.Find(StockId) == null)
        {
            return null;
        }
        var comment = commentDTO.ToCommentFromCreateDTO(StockId);
        await _dbContext.Comments.AddAsync(comment);
        await _dbContext.SaveChangesAsync();
        return comment;
    }

    public async Task<Comment?> GetById(int id)
    { 
        return await _dbContext.Comments.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<CommentDTO?> Update(int id, UpdateCommentRequestDTO update)
    {
        var _comment = await _dbContext.Comments.FindAsync(id);
        if (_comment == null)
        {
            return null;
        }
        _comment.Subject = update.Subject;
        _comment.Content = update.Content;
        await _dbContext.SaveChangesAsync();
        return _comment.ToCommentDTO();
    }

    public async Task<bool> CommentExists(int id)
    {
        return await _dbContext.Comments.AnyAsync(s => s.Id == id);
    }

    public async Task<Comment?> Delete(int id)
    {
        var _comment = await _dbContext.Comments.FirstOrDefaultAsync(c => c.Id == id);
        if (_comment == null)
        {
            return null;
        }
        _dbContext.Comments.Remove(_comment);
        await _dbContext.SaveChangesAsync();
        return(_comment);
    }
}
