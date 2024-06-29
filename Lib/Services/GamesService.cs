using DB.Daos;
using DB.Models;
using Lib.Exceptions;

namespace Lib.Services;

public class GamesService : IGamesService
{
    private readonly IGamesDao _gamesDao;
    
    public GamesService(IGamesDao gamesDao)
    {
        _gamesDao = gamesDao;
    }

    public async Task SaveSports(List<Game> sports)
    {
        await _gamesDao.SaveGames(sports);
    }

    public async Task<List<Game>> GetSports()
    {
        var games = await _gamesDao.GetGames();
        return games.OrderByDescending(g => g.GameTime).ToList();
    }
    
    public async Task<Game> GetSportsById(int id)
    {
        var sportsData = await _gamesDao.GetGameById(id);
        if (sportsData == null)
        {
            throw new NotFoundException($"Could not find sports data with id {id}");
        }

        return sportsData;
    }
}