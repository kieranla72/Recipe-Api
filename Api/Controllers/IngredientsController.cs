using Api.ResponseModels;
using DB.Models;
using Lib.Exceptions;
using Lib.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("Ingredients")]
public class IngredientsController : Controller
{
    private readonly IIngredientsService _ingredientsService;
    private readonly ICacheManagerService _cacheManagerService;
    
    public IngredientsController(IIngredientsService ingredientsService, ICacheManagerService cacheManagerService)
    {
        _ingredientsService = ingredientsService;
        _cacheManagerService = cacheManagerService;
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody]List<Ingredient> ingredients)
    {
        await _ingredientsService.SaveIngredients(ingredients);
        return Created("Created new ingredients", ingredients);
    }       
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var retrievalFunction = async () => await _ingredientsService.GetIngredients();
        var ingredients = await _cacheManagerService.TryGetCache(CacheKeys.IngredientsData, 20, retrievalFunction);
        var ingredientResponseDtos = ingredients.Select(i => new IngredientResponseDto(i)).ToList();

        return Ok(ingredientResponseDtos);
    }    
    
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var retrievalFunction = async () => await _ingredientsService.GetIngredientById(id);
            var ingredient = await _cacheManagerService.TryGetCache(CacheKeys.IngredientsData, 20, retrievalFunction);

            return Ok(new IngredientResponseDto(ingredient));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }
}