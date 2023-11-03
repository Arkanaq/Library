using Microsoft.AspNetCore.Mvc;
using TestApp.DbModel.Models;
using TestApp.Services;

namespace BookStoreApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    private readonly BookService _bookService;

    public BookController(BookService bookService) =>
        _bookService = bookService;

    [HttpGet]
    public async Task<List<Book>> Get() =>
        await _bookService.GetAsync();

    [HttpGet("id")]
    public async Task<ActionResult<Book>> Get(Guid id)
    {
        var book = await _bookService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        return book;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Book newBook)
    {
        await _bookService.CreateAsync(newBook);

        return CreatedAtAction(nameof(Get), new { id = newBook.Id }, newBook);
    }

    [HttpPut]
    public async Task<IActionResult> Update(Guid id, Book updatedBook)
    {
        var book = await _bookService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        updatedBook.Id = book.Id;

        await _bookService.UpdateAsync(id, updatedBook);

        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(Guid id)
    {
        var book = await _bookService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        await _bookService.RemoveAsync(id);

        return NoContent();
    }
}