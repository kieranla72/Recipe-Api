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

    public async Task SaveSports()
    {
        await _sportsDbContext.SportsData.AddAsync(new SportsData{HomeTeam = "homeTeam", AwayTeam = "awayTeam", GameTime = new DateTime()});
        await _sportsDbContext.SaveChangesAsync();
    }
    
    public async Task<List<SportsData>> GetSports()
    {
        var sportsData = await _sportsDbContext.SportsData.ToListAsync();
        return sportsData;
    }
    
    
}