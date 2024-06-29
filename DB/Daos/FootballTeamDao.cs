using DB.Models;
using Microsoft.EntityFrameworkCore;

namespace DB.Daos;

public class FootballTeamDao : IFootballTeamDao
{
    private readonly FootballDbContext _footballDbContext;
    private readonly DbSet<FootballTeam> _footballTeamTable;
    public FootballTeamDao(FootballDbContext footballDbContext)
    {
        _footballDbContext = footballDbContext;
        _footballTeamTable = footballDbContext.FootballTeams;
    }

    public Task SaveFootbalTeams(List<FootballTeam> footballTeams)
    {
        _footballTeamTable.AddRange(footballTeams);
        throw new NotImplementedException();
    }
}