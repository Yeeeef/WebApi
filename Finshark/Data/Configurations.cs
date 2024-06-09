using Microsoft.EntityFrameworkCore;
using Finshark.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Finshark.Data;


    internal class StockEntityTypeConfiguration  : IEntityTypeConfiguration<Stock>
{
    public void Configure(EntityTypeBuilder<Stock> builder)
    {

    }

}

    internal class CommentEntityTypeConfiguration  : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
    
    }

}
