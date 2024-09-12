using DB.Models;

namespace Lib.Services;

public interface IRecipeService
{
    Task<List<Recipe>> SaveRecipes(List<Recipe> recipes);
    Task<List<Recipe>> GetRecipes();
    Task<Recipe> GetRecipeById(int id);
    Task<Recipe> UpdateRecipe(Recipe updatedRecipe);

}