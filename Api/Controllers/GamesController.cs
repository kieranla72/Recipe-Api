using DB.Models;
using Lib.Exceptions;
using Lib.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("Games")]
public class GamesController : Controller
{
    private readonly IGamesService _gamesService;
    public GamesController(IGamesService gamesService)
    {
        _gamesService = gamesService;
    }
    
    [Route("")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody]List<Game> sportsData)
    {
        await _gamesService.SaveSports(sportsData);
        return Created("Created new games", sportsData);
    }       
    
    [Route("")]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var ans = await _gamesService.GetSports();
        return Ok(ans);
    }    
    [Route("{id}")]
    [HttpGet]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var ans = await _gamesService.GetSportsById(id);
            return Ok(ans);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }
}