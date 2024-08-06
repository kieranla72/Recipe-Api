using DB.Models;
using Lib.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("Recipe")]
public class RecipeController : Controller
{
    private readonly IRecipeService _recipeService;

    public RecipeController(IRecipeService recipeService)
    {
        _recipeService = recipeService;
    }

    [Route("")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] List<Recipe> recipes)
    {
        await _recipeService.SaveRecipes(recipes);
        return Created($"{recipes.Count} teams created", recipes);
    }
}