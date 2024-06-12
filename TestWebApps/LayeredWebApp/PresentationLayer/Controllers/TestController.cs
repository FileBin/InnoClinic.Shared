using InnoClinic.Shared.Domain.Models;
using InnoClinic.Shared.Exceptions.Models;
using InnoClinic.Shared.LayeredWebApp.ApplicationLayer;
using InnoClinic.Shared.LayeredWebApp.ApplicationLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.Shared.LayeredWebApp.PresentationLayer.Controllers;

[ApiController]
[Route("/api/test")]
public class TestController(TestCrudService testCrudService) : ControllerBase {
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] PageDesc pageDesc, CancellationToken cancellationToken) {
        var list = await testCrudService.GetPageAsync(pageDesc, cancellationToken);

        return Ok(list);
    }

    [HttpGet]
    [ProducesResponseType(typeof(TestEntityResponse), StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(ProblemDetails))]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetEntity([FromRoute] Guid id, CancellationToken cancellationToken) {
        var entity = await testCrudService.GetByIdAsync(id, cancellationToken);

        return Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRequest createRequest, CancellationToken cancellationToken) {
        var entity = await testCrudService.CreateAsync(createRequest, cancellationToken);

        return Ok(entity);
    }

    [HttpPut, HttpPatch]
    [Route("{id:guid}")]
    public async Task<IActionResult> Create([FromRoute] Guid id, [FromBody] UpdateRequest updateRequest, CancellationToken cancellationToken) {
        await testCrudService.UpdateAsync(id, updateRequest, cancellationToken);

        return Ok();
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken) {
        await testCrudService.DeleteAsync(id, cancellationToken);

        return Ok();
    }

    [Route("404")]
    [HttpGet]
    [HttpPost]
    [HttpPut]
    [HttpPatch]
    [HttpDelete]
    public IActionResult TestException() {
        _ = testCrudService.ToString();
        throw new NotFoundException();
    }
}
