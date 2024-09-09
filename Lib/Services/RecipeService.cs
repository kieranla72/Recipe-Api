using DB.Daos;
using DB.Models;
using Lib.Exceptions;

namespace Lib.Services;

public class RecipeService : IRecipeService
{
    private readonly IRecipesDao _recipesDao;

    public RecipeService(IRecipesDao recipesDao)
    {
        _recipesDao = recipesDao;
    }

    public async Task<List<Recipe>> SaveRecipes(List<Recipe> recipes)
    {
        var recipesWithoutIngredients = recipes.Select(r => new Recipe(r)).ToList();
        await _recipesDao.SaveRecipes(recipesWithoutIngredients);
        return recipesWithoutIngredients;
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

    public async Task<Recipe> GetRecipeById(int id)
    {
        var recipe = await _recipesDao.GetRecipeById(id);
        if (recipe == null)
        {
            throw new NotFoundException($"Could not find a recipe corresponding to id {id}");
        }

        return recipe;
    }
}