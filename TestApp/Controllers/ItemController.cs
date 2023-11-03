using Microsoft.AspNetCore.Mvc;
using System.Numerics;
using TestApp.DbModel.Models;
using TestApp.Services;

namespace BookStoreApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayerController : ControllerBase
{
    private readonly PlayerService _playerService;

    public PlayerController(PlayerService playerService) =>
        _playerService = playerService;

    [HttpGet]
    public async Task<List<Author>> Get() =>
        await _playerService.GetAsync();

    [HttpGet("id")]
    public async Task<ActionResult<Author>> Get(Guid id)
    {
        var player = await _playerService.GetAsync(id);

        if (player is null)
        {
            return NotFound();
        }

        return player;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Author newPlayer)
    {
        await _playerService.CreateAsync(newPlayer);

        return CreatedAtAction(nameof(Get), new { id = newPlayer.Id }, newPlayer);
    }

    [HttpPut]
    public async Task<IActionResult> Update(Guid id, Author updatedPlayer)
    {
        var book = await _playerService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        updatedPlayer.Id = book.Id;

        await _playerService.UpdateAsync(id, updatedPlayer);

        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(Guid id)
    {
        var book = await _playerService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        await _playerService.RemoveAsync(id);

        return NoContent();
    }
}