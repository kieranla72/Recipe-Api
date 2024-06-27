using DB.Models;
using Microsoft.EntityFrameworkCore;

namespace DB;

public class SportsDbContext: DbContext
{
    public DbSet<SportsData> SportsData { get; set; }
    
    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL("Server=127.0.0.1;Database=Sports;User=root;Password=example");
    }
}