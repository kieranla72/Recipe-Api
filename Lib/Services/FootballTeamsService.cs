using DB.Daos;
using DB.Models;

namespace Lib.Services;

public class FootballTeamsService : IFootballTeamsService
{
    private readonly IFootballTeamsDao _footballTeamsDao; 
    public FootballTeamsService(IFootballTeamsDao footballTeamsDao)
    {
        _footballTeamsDao = footballTeamsDao;
    }

    public async Task SaveFootballTeams(List<FootballTeam> footballTeams)
    {
        await _footballTeamsDao.SaveFootballTeams(footballTeams);
    }
}