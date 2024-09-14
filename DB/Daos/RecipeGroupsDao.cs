using DB.Models;
using Microsoft.EntityFrameworkCore;

namespace DB.Daos;

public class RecipeGroupsDao : IRecipeGroupsDao
{
    private readonly RecipeDbContext _recipeDbContext;
    private readonly DbSet<RecipeGroup> _recipeGroupsTable;
    
    public RecipeGroupsDao(RecipeDbContext recipeDbContext)
    {
        _recipeDbContext = recipeDbContext;
        _recipeGroupsTable = recipeDbContext.RecipeGroups;
    }
    
    public async Task<int> CreateRecipeGroup(RecipeGroup recipeGroup)
    {
        await _recipeGroupsTable.AddAsync(recipeGroup);
        return await _recipeDbContext.SaveChangesAsync();
    }
}