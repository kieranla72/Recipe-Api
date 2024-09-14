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
}