using Finshark.Data;
using Finshark.DTO;
using Finshark.Interfaces;
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
        return await _dbContext.Comments.ToListAsync();
    }

    public async Task<Comment> Create(Comment comment)
    {
        await _dbContext.Comments.AddAsync(comment);
        await _dbContext.SaveChangesAsync();
        return comment;
    }

    public async Task<Comment?> GetById(int id)
    { 
        return await _dbContext.Comments.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Comment> Update(Comment comment, UpdateCommentRequestDTO update)
    {
        comment.Subject = update.Subject;
        comment.Content = update.Content;
        await _dbContext.SaveChangesAsync();
        return comment;
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
