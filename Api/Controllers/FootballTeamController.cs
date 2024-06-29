using DB.Models;
using Lib.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("FootballTeams")]
public class FootballTeamController : Controller
{
    private readonly IFootballTeamsService _footballTeamsService;

    public FootballTeamController(IFootballTeamsService footballTeamsService)
    {
        _footballTeamsService = footballTeamsService;
    }

    [Route("")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] List<FootballTeam> footballTeams)
    {
        await _footballTeamsService.SaveFootballTeams(footballTeams);
        return Created($"{footballTeams.Count} teams created", footballTeams);
    }
}