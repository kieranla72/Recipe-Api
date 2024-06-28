using DB.Models;
using Microsoft.EntityFrameworkCore;

namespace DB;

public class SportsDbContext: DbContext
{
    public DbSet<SportsData> SportsData { get; set; }

    public SportsDbContext(DbContextOptions<SportsDbContext> options)
        : base(options)
    {
    }

}