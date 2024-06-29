using DB.Models;

namespace Lib.Services;

public interface IGamesService
{
    Task SaveSports(List<Game> sports);
    Task<List<Game>> GetSports();
    Task<Game> GetSportsById(int id);
}