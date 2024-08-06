using DB.Models;

namespace Lib.Services;

public interface IRecipeService
{
    Task SaveRecipes(List<Recipe> footballTeams);
}