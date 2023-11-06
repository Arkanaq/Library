using Microsoft.AspNetCore.Mvc;
using Library.DbModel.Models;
using Library.Services;

namespace BookStoreApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    private readonly BookService _bookService;
    private readonly AuthorService _authorService;

    public BookController(BookService bookService, AuthorService authorService)
    {
        _bookService = bookService;
        _authorService = authorService;
    }


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
        var author = await _authorService.GetAsync(newBook.Author.FullName);

        if (author is null) await _authorService.CreateAsync(newBook.Author);

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

        book.Author = updatedBook.Author;
        book.Content = updatedBook.Content;
        book.Title = updatedBook.Title;

        await _bookService.UpdateAsync(book);

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

        await _bookService.RemoveAsync(book);

        return NoContent();
    }
}