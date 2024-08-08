using DB.Daos;
using DB.Models;

namespace Lib.Services;

public class RecipeService : IRecipeService
{
    private readonly IRecipesDao _RecipesDao; 
    public RecipeService(IRecipesDao recipesDao)
    {
        _RecipesDao = recipesDao;
    }

    public async Task SaveRecipes(List<Recipe> recipes)
    {
        await _RecipesDao.SaveRecipes(recipes);
    }
}