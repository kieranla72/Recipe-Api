using DB.Models;
using Microsoft.EntityFrameworkCore;

namespace DB.Daos;

public class RecipeGroupsDao : IRecipeGroupsDao
{
    private readonly RecipeDbContext _recipeDbContext;
    private readonly DbSet<RecipeGroup> _recipeGroupsTable;
    private readonly DbSet<RecipeGroupRecipe> _recipeGroupRecipesTable;
    
    public RecipeGroupsDao(RecipeDbContext recipeDbContext)
    {
        _recipeDbContext = recipeDbContext;
        _recipeGroupsTable = recipeDbContext.RecipeGroups;
        _recipeGroupRecipesTable = recipeDbContext.RecipeGroupRecipes;
    }
    
    public async Task<int> CreateRecipeGroup(RecipeGroup recipeGroup)
    {
        await _recipeGroupsTable.AddAsync(recipeGroup);
        return await _recipeDbContext.SaveChangesAsync();
    }

    public async Task<int> AddRecipeToRecipeGroup(RecipeGroupRecipe recipeGroupRecipe)
    {
        await _recipeGroupRecipesTable.AddAsync(recipeGroupRecipe);
        return await _recipeDbContext.SaveChangesAsync();
    }

    public async Task<List<RecipeGroup>> GetRecipeGroups()
    {
        var recipeGroups = await _recipeGroupsTable.ToListAsync();
        return recipeGroups;
    }
}