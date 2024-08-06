using DB.Models;
using Microsoft.EntityFrameworkCore;

namespace DB;

public class FootballDbContext: DbContext
{
    public DbSet<Game> Games { get; set; }
    public DbSet<Recipe> FootballTeams { get; set; }

    public FootballDbContext(DbContextOptions<FootballDbContext> options)
        : base(options)
    {
    }

}