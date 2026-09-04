using LibraryApp.Services;
using LibraryApp.View_Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Controllers;

[Route("api/authors")]
[ApiController]
public class AuthorsController : ControllerBase
{
    private readonly AuthorServices _service;
    public AuthorsController(AuthorServices service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? search, CancellationToken token) =>
        Ok(await _service.GetAllAsync(search, token));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken token)
    {
        var author = await _service.GetByIdAsync(id, token);
        return author is null ? NotFound() : Ok(author);
    }

    [Authorize, HttpPost]
    public async Task<IActionResult> Create(AuthorVm request, CancellationToken token)
    {
        var author = await _service.CreateAsync(request, token);
        return CreatedAtAction(nameof(GetById), new { id = author.Id }, author);
    }

    [Authorize, HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, AuthorVm request, CancellationToken token)
    {
        var author = await _service.UpdateAsync(id, request, token);
        return author is null ? NotFound() : Ok(author);
    }

    [Authorize, HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken token) =>
        await _service.DeleteAsync(id, token) ? NoContent() : NotFound();
}
