using DB.Models;

namespace DB.Daos;

internal interface IFootballTeamDao
{
    Task SaveFootbalTeams(List<FootballTeam> footballTeams);
}