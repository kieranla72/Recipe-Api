using DB.Models;

namespace Lib.Services;

public interface ISportsService
{
    Task SaveSports(List<SportsData> sports);
    Task<List<SportsData>> GetSports();
    Task<SportsData> GetSportsById(int id);
}