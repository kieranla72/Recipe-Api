using DB.Models;

namespace DB.Daos;

public interface ISportsDao
{
    Task SaveSports(List<SportsData> sportsData);
    Task<List<SportsData>> GetSports();
    Task<SportsData?> GetSportsById(int id);
}