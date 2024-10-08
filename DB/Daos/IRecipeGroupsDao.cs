using DB.Models;

namespace DB.Daos;

public interface IRecipeGroupsDao
{
    Task<int> CreateRecipeGroup(RecipeGroup recipeGroup);
    Task<int> AddRecipeToRecipeGroup(RecipeGroupRecipe recipeGroupRecipe);
    Task<List<RecipeGroup>> GetRecipeGroups();
    Task<List<RecipeGroup>> SearchRecipeGroupsByTitle(string title);
}
