using LibraryApp.Services;
using LibraryApp.View_Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Controllers;

[Route("api/books")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly BooksService _service;
    public BooksController(BooksService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? search, CancellationToken token) =>
        Ok(await _service.GetAllAsync(search, token));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken token)
    {
        var book = await _service.GetByIdAsync(id, token);
        return book is null ? NotFound() : Ok(book);
    }

    [Authorize, HttpPost]
    public async Task<IActionResult> Create(BookVm request, CancellationToken token)
    {
        try
        {
            var book = await _service.CreateAsync(request, token);
            return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
        }
        catch (ArgumentException exception)
        {
            return BadRequest(new { error = exception.Message });
        }
    }

    [Authorize, HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, BookVm request, CancellationToken token)
    {
        try
        {
            var book = await _service.UpdateAsync(id, request, token);
            return book is null ? NotFound() : Ok(book);
        }
        catch (ArgumentException exception)
        {
            return BadRequest(new { error = exception.Message });
        }
    }

    [Authorize, HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken token) =>
        await _service.DeleteAsync(id, token) ? NoContent() : NotFound();
}
