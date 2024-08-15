using DB.Daos;
using DB.Models;

namespace Lib.Services;

public class RecipeService : IRecipeService
{
    private readonly IRecipesDao _recipesDao; 
    public RecipeService(IRecipesDao recipesDao)
    {
        _recipesDao = recipesDao;
    }

    public async Task SaveRecipes(List<Recipe> recipes)
    {
        await _recipesDao.SaveRecipes(recipes);
    }

    public async Task<List<Recipe>> GetRecipes()
    {
        var recipes = await _recipesDao.GetRecipes();
        return recipes.OrderBy(r => r.Title).ToList();
    }
}