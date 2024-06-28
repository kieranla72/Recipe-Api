using DB.Daos;
using DB.Models;
using Lib.Exceptions;

namespace Lib.Services;

public class SportsService : ISportsService
{
    private readonly ISportsDao _sportsDao;
    
    public SportsService(ISportsDao sportsDao)
    {
        _sportsDao = sportsDao;
    }

    public async Task SaveSports(List<SportsData> sports)
    {
        await _sportsDao.SaveSports(sports);
    }

    public async Task<List<SportsData>> GetSports()
    {
        return await _sportsDao.GetSports();
    }
    
    public async Task<SportsData> GetSportsById(int id)
    {
        var sportsData = await _sportsDao.GetSportsById(id);
        if (sportsData == null)
        {
            throw new NotFoundException($"Could not find sports data with id {id}");
        }

        return sportsData;
    }
}