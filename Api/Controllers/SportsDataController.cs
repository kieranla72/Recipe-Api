using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class SportsDataController : Controller
{
    [Route("Upload")]
    [HttpPost]
    public IActionResult Create()
    {
        return Ok("asdfadsfasd");
    }
}