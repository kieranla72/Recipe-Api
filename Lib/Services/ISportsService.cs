using DB.Models;

namespace Lib.Services;

public interface ISportsService
{
    Task<string> SaveSports(List<SportsData> sports);
    Task<List<SportsData>> GetSports();
}