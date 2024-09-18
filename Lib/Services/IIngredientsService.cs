
using DB.Models;

namespace Lib.Services;

public interface IIngredientsService
{
    Task<int> SaveIngredients(List<Ingredient> ingredients);
    Task<List<Ingredient>> GetIngredients();
    Task<Ingredient> GetIngredientById(int id);
    Task<List<Ingredient>> SearchIngredientsByTitle(string title);

}