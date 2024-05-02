using InnoClinic.Shared.Exceptions.Models;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.Shared.LayeredWebApp.PresentationLayer.Controllers;

[ApiController]
[Route("/api/test")]
public class TestController : ControllerBase {
    [Route("404")]
    [HttpGet]
    [HttpPost]
    [HttpPut]
    [HttpPatch]
    [HttpDelete]
    public async Task<IActionResult> TestException() {
        throw new NotFoundException();
    }
}
