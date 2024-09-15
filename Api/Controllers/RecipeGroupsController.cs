using Api.ResponseModels;
using DB.Models;
using Lib.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("RecipeGroups")]
public class RecipeGroupsController : Controller
{
    private readonly IRecipeGroupsService _recipeGroupsService;

    public RecipeGroupsController(IRecipeGroupsService recipeGroupsService)
    {
        _recipeGroupsService = recipeGroupsService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RecipeGroup recipeGroup)
    {
        await _recipeGroupsService.CreateRecipeGroup(recipeGroup);
        return Created(recipeGroup.Id.ToString(), recipeGroup);
    }
    
    [HttpPost("{recipeGroupId}")]
    public async Task<IActionResult> Create(int recipeGroupId, [FromBody] int recipeId)
    {
        await _recipeGroupsService.AddRecipeToRecipeGroup(recipeGroupId, recipeId);
        return Created();
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var recipeGroups = await _recipeGroupsService.GetRecipeGroups();
        var recipeGroupResponseDtos = recipeGroups.Select(rg => new RecipeGroupResponseDto(rg));
        return Ok(recipeGroupResponseDtos);
    }
}