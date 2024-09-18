using DB.Daos;
using DB.Models;

namespace Lib.Services;

public class RecipeGroupsService: IRecipeGroupsService
{
    private readonly IRecipeGroupsDao _recipeGroupsDao;

    public RecipeGroupsService(IRecipeGroupsDao recipeGroupsDao)
    {
        _recipeGroupsDao = recipeGroupsDao;
    }
    
    public Task<int> CreateRecipeGroup(RecipeGroup recipeGroup)
    {
        return _recipeGroupsDao.CreateRecipeGroup(recipeGroup);
    }

    public Task<int> AddRecipeToRecipeGroup(int groupId, int recipeId)
    {
        var recipeGroupRecipe = new RecipeGroupRecipe { RecipeGroupId = groupId, RecipeId = recipeId };
        return _recipeGroupsDao.AddRecipeToRecipeGroup(recipeGroupRecipe);
    }

    public Task<List<RecipeGroup>> GetRecipeGroups()
    {
        return _recipeGroupsDao.GetRecipeGroups();
    }

    public Task<List<RecipeGroup>> SearchRecipeGroupsByTitle(string title)
    {
        return _recipeGroupsDao.SearchRecipeGroupsByTitle(title);
    }
}