using DB.Daos;
using DB.Models;

namespace Lib.Services;

public class RecipeService : IRecipeService
{
    private readonly IRecipesDao _recipesDao;
    private readonly IRecipeIngredientsDao _recipeIngredientsDao;

    public RecipeService(IRecipesDao recipesDao, IRecipeIngredientsDao recipeIngredientsDao)
    {
        _recipesDao = recipesDao;
        _recipeIngredientsDao = recipeIngredientsDao;
    }

    public async Task SaveRecipes(List<Recipe> recipes)
    {
        await _recipesDao.SaveRecipes(recipes);
        // var recipesToInsert = GetRecipeIngredients(recipes);
        // await _recipeIngredientsDao.SaveRecipeIngredients(recipesToInsert);
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