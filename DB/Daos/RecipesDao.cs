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

    public async Task<List<Recipe>> GetRecipes()
    {
        var recipes = await _recipesTable.Include(r => r.Ingredients).ToListAsync();
        return recipes;
    }

    public async Task<Recipe?> GetRecipeById(int id)
    {
        var recipe = await _recipesTable.FindAsync(id);
        return recipe;
    }

    public async Task<Recipe> UpdateRecipe(Recipe updatedRecipe)
    {
        _recipesTable.Update(updatedRecipe);
        return updatedRecipe;
    }
}