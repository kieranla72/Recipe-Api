using System.ComponentModel.DataAnnotations;
using DB.Models;
using Lib.Exceptions;
using Lib.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
public class GameAnalysisController : Controller
{
    private readonly ISportsService _sportsService;
    public GameAnalysisController(ISportsService sportsService)
    {
        _sportsService = sportsService;
    }
    
    [Route("")]
    [HttpPost]
    public async Task<IActionResult> ProcessData([FromBody]List<SportsData> sportsData)
    {
        await _sportsService.SaveSports(sportsData);
        return Created();
    }       
    
    [Route("")]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var ans = await _sportsService.GetSports();
        return Ok(ans);
    }    
    [Route("/{id}")]
    [HttpGet]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var ans = await _sportsService.GetSportsById(id);
            return Ok(ans);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }
}