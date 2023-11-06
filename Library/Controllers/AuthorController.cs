using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using System.Numerics;
using Library.DbModel.Models;
using Library.Services;

namespace BookStoreApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorController : ControllerBase
{
    private readonly AuthorService _authorService;

    public AuthorController(AuthorService authorService) =>
        _authorService = authorService;


    [HttpGet]
    [EnableQuery]
    public ActionResult<Author> GetData(){
       return Ok(_authorService.GetAsync());
    }

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
        newAuthor.EditTime = DateTime.Now;
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

        author.Name = updatedAuthor.Name;
        author.SurName = updatedAuthor.SurName;
        author.EditTime = DateTime.Now;

        await _authorService.UpdateAsync(updatedAuthor);

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

        await _authorService.RemoveAsync(book);

        return NoContent();
    }
}