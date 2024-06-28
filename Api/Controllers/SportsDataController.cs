using System.ComponentModel.DataAnnotations;
using DB.Models;
using Lib.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
public class SportsDataController : Controller
{
    private readonly ISportsService _sportsService;
    public SportsDataController(ISportsService sportsService)
    {
        _sportsService = sportsService;
    }
    
    [Route("ProcessData")]
    [HttpPost]
    public async Task<IActionResult> ProcessData([FromBody]List<SportsData> sportsData)
    {
        var ans = await _sportsService.SaveSports(sportsData);
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