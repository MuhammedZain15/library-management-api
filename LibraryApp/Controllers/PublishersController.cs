using LibraryApp.Services;
using LibraryApp.View_Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Controllers;

[Route("api/publishers")]
[ApiController]
public class PublishersController : ControllerBase
{
    private readonly PublisherServices _service;
    public PublishersController(PublisherServices service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? search, CancellationToken token) =>
        Ok(await _service.GetAllAsync(search, token));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken token)
    {
        var publisher = await _service.GetByIdAsync(id, token);
        return publisher is null ? NotFound() : Ok(publisher);
    }

    [Authorize, HttpPost]
    public async Task<IActionResult> Create(PublisherVM request, CancellationToken token)
    {
        var publisher = await _service.CreateAsync(request, token);
        return CreatedAtAction(nameof(GetById), new { id = publisher.Id }, publisher);
    }

    [Authorize, HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, PublisherVM request, CancellationToken token)
    {
        var publisher = await _service.UpdateAsync(id, request, token);
        return publisher is null ? NotFound() : Ok(publisher);
    }

    [Authorize, HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken token) =>
        await _service.DeleteAsync(id, token) ? NoContent() : NotFound();
}
