using DB.Models;
using Lib.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("Recipes")]
public class RecipesController : Controller
{
    private readonly IRecipeService _recipeService;

    public RecipesController(IRecipeService recipeService)
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
    
    [Route("")]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var recipes = await _recipeService.GetRecipes();
        return Ok(recipes);
    }
}