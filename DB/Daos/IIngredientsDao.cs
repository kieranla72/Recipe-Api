using DB.Models;

namespace DB.Daos;

public interface IIngredientsDao
{
    Task<int> SaveIngredients(List<Ingredient> ingredients);
    Task<List<Ingredient>> GetIngredients();
    Task<Ingredient?> GetIngredientsById(int id);
}