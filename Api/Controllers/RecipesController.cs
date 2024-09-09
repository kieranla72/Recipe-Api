using Api.ResponseModels;
using DB.Models;
using Lib.Exceptions;
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
        try
        {
            var insertedRecipes = await _recipeService.SaveRecipes(recipes);
            return Created("s", insertedRecipes);
        }
        catch (Exception e)
        {
            Console.WriteLine("There was an error with saving the recipe");
            Console.WriteLine(e);
            throw;
        }
    }

    [Route("")]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var recipes = await _recipeService.GetRecipes();
        var recipeDtos = recipes.Select(ProjectRecipeModel).ToList();
        return Ok(recipeDtos);
    }
    
    [Route("{id}")]
    [HttpGet]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var recipes = await _recipeService.GetRecipeById(id);
            return Ok(recipes);

        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    private RecipeResponseDto ProjectRecipeModel(Recipe recipe) => new(recipe);
}