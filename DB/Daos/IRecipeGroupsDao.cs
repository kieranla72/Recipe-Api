using DB.Models;

namespace DB.Daos;

public interface IRecipeGroupsDao
{
    Task<int> CreateRecipeGroup(RecipeGroup recipeGroup);
}
