using DB.Models;

namespace DB.Daos;

public interface IRecipeIngredientsDao
{
    Task<int> SaveRecipeIngredients(List<RecipeIngredient> recipeIngredients);
}