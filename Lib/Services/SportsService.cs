using DB.Daos;
using DB.Models;

namespace Lib.Services;

public class SportsService : ISportsService
{
    private readonly ISportsDao _sportsDao;
    
    public SportsService(ISportsDao sportsDao)
    {
        _sportsDao = sportsDao;
    }

    public async Task<string> SaveSports(List<SportsData> sports)
    {
        await _sportsDao.SaveSports(sports);
        return "hello there mate";
    }

    public async Task<List<SportsData>> GetSports()
    {
        return await _sportsDao.GetSports();
    }
}