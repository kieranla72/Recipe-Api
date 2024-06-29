using DB.Models;

namespace Lib.Services;

public interface IFootballTeamsService
{
    Task SaveFootballTeams(List<FootballTeam> footballTeams);
}