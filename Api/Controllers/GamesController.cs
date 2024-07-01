using DB.Models;
using Lib.Exceptions;
using Lib.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Api.Controllers;

[ApiController]
[Route("Games")]
public class GamesController : Controller
{
    private readonly IGamesService _gamesService;
    private readonly ICacheManagerService _cacheManagerService;
    
    public GamesController(IGamesService gamesService, ICacheManagerService cacheManagerService)
    {
        _gamesService = gamesService;
        _cacheManagerService = cacheManagerService;
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody]List<Game> sportsData)
    {
        await _gamesService.SaveSports(sportsData);
        return Created("Created new games", sportsData);
    }       
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var retrievalFunction = async () => await _gamesService.GetSports();
        var games = await _cacheManagerService.TryGetCache(CacheKeys.GamesData, 20, retrievalFunction);

        return Ok(games);
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