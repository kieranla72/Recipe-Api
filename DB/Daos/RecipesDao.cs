using DB.Models;
using Microsoft.EntityFrameworkCore;

namespace DB.Daos;

public class RecipesDao : IRecipesDao
{
    private readonly RecipeDbContext _recipeDbContext;
    private readonly DbSet<Recipe> _recipesTable;
    public RecipesDao(RecipeDbContext recipeDbContext)
    {
        _recipeDbContext = recipeDbContext;
        _recipesTable = recipeDbContext.Recipes;
    }

    public async Task SaveRecipes(List<Recipe> recipes)
    {
        _recipesTable.AddRange(recipes);
        await _recipeDbContext.SaveChangesAsync();
    }
}