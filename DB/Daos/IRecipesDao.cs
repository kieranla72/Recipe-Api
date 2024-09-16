using DB.Models;

namespace DB.Daos;

public interface IRecipesDao
{
    Task SaveRecipes(List<Recipe> recipes);
    Task<List<Recipe>> GetRecipes();
    Task<Recipe?> GetRecipeById(int id);
    Task<Recipe> UpdateRecipe(Recipe updatedRecipe);
    Task<List<Recipe>> GetRecipesByRecipeGroup(int recipeGroupId);
}