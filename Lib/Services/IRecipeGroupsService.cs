using DB.Models;

namespace Lib.Services;

public interface IRecipeGroupsService
{
    public Task<int> CreateRecipeGroup(RecipeGroup recipeGroup);
    public Task<int> AddRecipeToRecipeGroup(int groupId, int recipeId);
}