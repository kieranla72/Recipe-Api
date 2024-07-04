using DB.Models;

namespace DB.Daos;

public interface IGamesDao
{
    Task<int> SaveGames(List<Game> sportsData);
    Task<List<Game>> GetGames();
    Task<Game?> GetGameById(int id);
}