using DB.Models;
using Microsoft.EntityFrameworkCore;

namespace DB.Daos;

public class FootballTeamsDao : IFootballTeamsDao
{
    private readonly FootballDbContext _footballDbContext;
    private readonly DbSet<Recipe> _footballTeamTable;
    public FootballTeamsDao(FootballDbContext footballDbContext)
    {
        _footballDbContext = footballDbContext;
        _footballTeamTable = footballDbContext.FootballTeams;
    }

    public async Task SaveFootballTeams(List<Recipe> footballTeams)
    {
        _footballTeamTable.AddRange(footballTeams);
        await _footballDbContext.SaveChangesAsync();
    }
}