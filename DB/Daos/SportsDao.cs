using DB.Models;
using Microsoft.EntityFrameworkCore;

namespace DB.Daos;

public class SportsDao : ISportsDao
{
    private readonly SportsDbContext _sportsDbContext;
    private readonly DbSet<SportsData> _sportsData;

    public SportsDao(SportsDbContext sportsDbContext)
    {
        _sportsDbContext = sportsDbContext;
        _sportsData = sportsDbContext.SportsData;
    }

    public async Task SaveSports(List<SportsData> sportsData)
    {
        await _sportsData.AddRangeAsync(sportsData);
        await _sportsDbContext.SaveChangesAsync();
    }
    
    public async Task<List<SportsData>> GetSports()
    {
        var sportsData = await _sportsData.ToListAsync();
        return sportsData;
    }    
    public async Task<SportsData?> GetSportsById(int id)
    {
        var sportsData = await _sportsData.FindAsync(id);
        return sportsData;
    }
}