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

    public async Task<Recipe> UpdateRecipe(Recipe updatedRecipe)
    {
        return await _recipesDao.UpdateRecipe(updatedRecipe);
    }

    public Task<List<Recipe>> GetRecipesByRecipeGroup(int recipeGroupId)
    {
        return _recipesDao.GetRecipesByRecipeGroup(recipeGroupId);
    }
}