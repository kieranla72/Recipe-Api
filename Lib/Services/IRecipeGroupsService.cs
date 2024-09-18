using DB.Models;

namespace Lib.Services;

public interface IRecipeGroupsService
{
    Task<int> CreateRecipeGroup(RecipeGroup recipeGroup);
    Task<int> AddRecipeToRecipeGroup(int groupId, int recipeId);
    Task<List<RecipeGroup>> GetRecipeGroups();
    Task<List<RecipeGroup>> SearchRecipeGroupsByTitle(string title);
}