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
        var insertedRecipes = await _recipeService.SaveRecipes(recipes);
        return Created("s", insertedRecipes);
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

    [Route("")]
    [HttpPut]
    public async Task<IActionResult> Put([FromBody] Recipe updatedRecipe)
    {
        await _recipeService.UpdateRecipe(updatedRecipe);
        return Created("s", updatedRecipe);
    }
    
    [Route("RecipeGroups/{recipeGroupId}")]
    [HttpGet]
    public async Task<IActionResult> Put(int recipeGroupId)
    {
        var recipes = await _recipeService.GetRecipesByRecipeGroup(recipeGroupId);
        return Ok(recipes);
    }

    private RecipeResponseDto ProjectRecipeModel(Recipe recipe) => new(recipe);
}