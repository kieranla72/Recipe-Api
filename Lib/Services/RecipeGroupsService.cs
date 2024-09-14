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
}