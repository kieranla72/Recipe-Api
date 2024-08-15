using DB.Models;

namespace Lib.Services;

public interface IRecipeService
{
    Task SaveRecipes(List<Recipe> recipes);
    Task<List<Recipe>> GetRecipes();
}