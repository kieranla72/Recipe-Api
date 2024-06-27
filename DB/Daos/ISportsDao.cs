using DB.Models;

namespace DB.Daos;

public interface ISportsDao
{
    Task SaveSports();
    Task<List<SportsData>> GetSports();
}