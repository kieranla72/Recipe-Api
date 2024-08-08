using DB.Models;
using Microsoft.EntityFrameworkCore;

namespace DB.Daos;

public class IngredientsDao : IIngredientsDao
{
    private readonly RecipeDbContext _recipeDbContext;
    private readonly DbSet<Ingredient> _ingredientsTable;

    public IngredientsDao(RecipeDbContext recipeDbContext)
    {
        _recipeDbContext = recipeDbContext;
        _ingredientsTable = recipeDbContext.Ingredients;
    }

    public async Task<int> SaveIngredients(List<Ingredient> ingredients)
    {
        await _ingredientsTable.AddRangeAsync(ingredients);
        return await _recipeDbContext.SaveChangesAsync();
    }
    
    public async Task<List<Ingredient>> GetIngredients()
    {
        var ingredients = await
            _ingredientsTable.ToListAsync();
        return ingredients;
    }    
    public async Task<Ingredient?> GetIngredientsById(int id)
    {
        var ingredient = await _ingredientsTable.FindAsync(id);
        return ingredient;
    }
}