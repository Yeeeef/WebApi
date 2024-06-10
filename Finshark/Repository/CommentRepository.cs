using Finshark.Data;
using Finshark.Interfaces;
using Finshark.Models;
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
    
    public async Task<List<Comment>> GetAllAsync()
    { 
        return await _dbContext.Comments.ToListAsync();
    }

    public async Task<Comment> CreateAsync(Comment comment)
    {
        await _dbContext.Comments.AddAsync(comment);
        await _dbContext.SaveChangesAsync();
        return comment;
    }

    public async Task<Comment?> GetByIdAsync (int id)
    { 
        return await _dbContext.Comments.FirstOrDefaultAsync(c => c.Id == id);
    }

}
