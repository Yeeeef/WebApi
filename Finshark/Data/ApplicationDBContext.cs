using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Finshark.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Finshark.Data;

public class ApplicationDBContext : DbContext
{
    public ApplicationDBContext(DbContextOptions dbContextOptions)
    : base(dbContextOptions)
    {
        
    }


    public DbSet<Stock> Stocks { get; set; }
    public DbSet<Comment> Comments { get; set; }
}
