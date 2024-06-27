using DB.Models;
using Lib.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class SportsDataController : Controller
{
    private readonly ISportsService _sportsService;
    public SportsDataController(ISportsService sportsService)
    {
        _sportsService = sportsService;
    }
    
    [Route("Upload")]
    [HttpPost]
    public async Task<IActionResult> Create()
    {
        var ans = await _sportsService.SaveSports(new ());
        return Ok(ans);
    }   
    
    [Route("Get")]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var ans = await _sportsService.GetSports();
        return Ok(ans);
    }
}