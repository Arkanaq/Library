using Microsoft.AspNetCore.Mvc;
using System.Numerics;
using TestApp.DbModel.Models;
using TestApp.Services;

namespace BookStoreApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorController : ControllerBase
{
    private readonly AuthorService _authorService;

    public AuthorController(AuthorService authorService) =>
        _authorService = authorService;

    [HttpGet]
    public async Task<List<Author>> Get() =>
        await _authorService.GetAsync();

    [HttpGet("id")]
    public async Task<ActionResult<Author>> Get(Guid id)
    {
        var author = await _authorService.GetAsync(id);

        if (author is null)
        {
            return NotFound();
        }

        return author;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Author newAuthor)
    {
        await _authorService.CreateAsync(newAuthor);

        return CreatedAtAction(nameof(Get), new { id = newAuthor.Id }, newAuthor);
    }

    [HttpPut]
    public async Task<IActionResult> Update(Guid id, Author updatedAuthor)
    {
        var author = await _authorService.GetAsync(id);

        if (author is null)
        {
            return NotFound();
        }

        updatedAuthor.Id = author.Id;

        await _authorService.UpdateAsync(id, updatedAuthor);

        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(Guid id)
    {
        var book = await _authorService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        await _authorService.RemoveAsync(id);

        return NoContent();
    }
}