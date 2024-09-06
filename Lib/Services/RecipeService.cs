using DB.Daos;
using DB.Models;

namespace Lib.Services;

public class RecipeService : IRecipeService
{
    private readonly IRecipesDao _recipesDao;

    public RecipeService(IRecipesDao recipesDao)
    {
        _recipesDao = recipesDao;
    }

    public async Task SaveRecipes(List<Recipe> recipes)
    {
        await _recipesDao.SaveRecipes(recipes);
    }

    private List<RecipeIngredient> GetRecipeIngredients(List<Recipe> recipes)
    {
        var recipeIngredients = new List<RecipeIngredient>();

        foreach (var recipe in recipes)
        {
            if (recipe.RecipeIngredients.Count == 0) continue;
            
            foreach (var recipeIngredient in recipe.RecipeIngredients)
            {
                recipeIngredients.Add(new RecipeIngredient(recipeIngredient, recipe.Id));
            }
        }

        return recipeIngredients;
    }

    public async Task<List<Recipe>> GetRecipes()
    {
        var recipes = await _recipesDao.GetRecipes();
        return recipes.OrderBy(r => r.Title).ToList();
    }
}