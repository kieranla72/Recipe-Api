using DB.Models;
using Microsoft.EntityFrameworkCore;

namespace DB.Daos;

public class RecipeIngredientsDao : IRecipeIngredientsDao
{
    private readonly RecipeDbContext _recipeDbContext;
    private readonly DbSet<RecipeIngredient> _recipeIngredientsTable;
    
    public RecipeIngredientsDao(RecipeDbContext recipeDbContext)
    {
        _recipeDbContext = recipeDbContext;
        _recipeIngredientsTable = recipeDbContext.RecipeIngredients;
    }
    
    public async Task<int> SaveRecipeIngredients(List<RecipeIngredient> recipeIngredients)
    {
        await _recipeIngredientsTable.AddRangeAsync(recipeIngredients);
        return await _recipeDbContext.SaveChangesAsync();
    }
}