using DB.Daos;
using DB.Models;
using Lib.Exceptions;

namespace Lib.Services;

public class IngredientsService : IIngredientsService
{
    private readonly IIngredientsDao _ingredientsDao;
    private int iterations = 10000000;
    
    public IngredientsService(IIngredientsDao ingredientsDao)
    {
        _ingredientsDao = ingredientsDao;
    }

    public async Task<int> SaveIngredients(List<Ingredient> ingredients)
    {
        var savedIngredients = await _ingredientsDao.SaveIngredients(ingredients);
        return savedIngredients;
    }
    public async Task<List<Ingredient>> GetIngredients()
    {
        var ingredients = await _ingredientsDao.GetIngredients();
        return ingredients.OrderByDescending(g => g.Title).ToList();
    }
    
    public async Task<Ingredient> GetIngredientById(int id)
    {
        var sportsData = await _ingredientsDao.GetIngredientsById(id);
        if (sportsData == null)
        {
            throw new NotFoundException($"Could not find sports data with id {id}");
        }

        return sportsData;
    }
}

