using DB.Daos;
using DB.Models;

namespace Lib.Services;

public class RecipeService : IRecipeService
{
    private readonly IFootballTeamsDao _footballTeamsDao; 
    public RecipeService(IFootballTeamsDao footballTeamsDao)
    {
        _footballTeamsDao = footballTeamsDao;
    }

    public async Task SaveRecipes(List<Recipe> footballTeams)
    {
        await _footballTeamsDao.SaveFootballTeams(footballTeams);
    }
}