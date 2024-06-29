using DB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace DB.Daos;

public class GamesDao : IGamesDao
{
    private readonly FootballDbContext _footballDbContext;
    private readonly DbSet<Game> _gameTable;

    public GamesDao(FootballDbContext footballDbContext)
    {
        _footballDbContext = footballDbContext;
        _gameTable = footballDbContext.Games;
    }

    public async Task SaveGames(List<Game> sportsData)
    {
        await _gameTable.AddRangeAsync(sportsData);
        await _footballDbContext.SaveChangesAsync();
    }
    
    // public async Task<List<Game>> GetGames()
    // {
    //     var sportsData = await
    //         _gameTable.FromSql(
    //             $"SELECT Id, HomeTeamId, AwayTeamId, HomeTeamScore, AwayTeamScore, GameTime FROM Games ORDER BY GameTime Desc")
    //             .ToListAsync();
    //     return sportsData;
    // }   
    
    public async Task<List<Game>> GetGames()
    {
        var sportsData = await
            _gameTable.ToListAsync();
        return sportsData;
    }    
    public async Task<Game?> GetGameById(int id)
    {
        var sportsData = await _gameTable.FindAsync(id);
        return sportsData;
    }
}