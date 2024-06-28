using DB.Models;
using Microsoft.EntityFrameworkCore;

namespace DB.Daos;

public class SportsDao : ISportsDao
{
    private readonly SportsDbContext _sportsDbContext;

    public SportsDao(SportsDbContext sportsDbContext)
    {
        _sportsDbContext = sportsDbContext;
    }

    public async Task SaveSports(List<SportsData> sportsData)
    {
        await _sportsDbContext.SportsData.AddRangeAsync(sportsData);
        await _sportsDbContext.SaveChangesAsync();
    }
    
    public async Task<List<SportsData>> GetSports()
    {
        // return new();
        var sportsData = await _sportsDbContext.SportsData.ToListAsync();
        return sportsData;
    }
    
    
}