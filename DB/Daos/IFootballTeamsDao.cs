using DB.Models;

namespace DB.Daos;

public interface IFootballTeamsDao
{
    Task SaveFootballTeams(List<Recipe> footballTeams);
}