using DB.Models;

namespace Lib.Services;

public interface IGamesService
{
    Task<int> SaveSports(List<GameCreateDto> sports);
    Game CalculateGameStatistics(GameCreateDto sports);
    Task<List<Game>> GetSports();
    Task<Game> GetSportsById(int id);
}