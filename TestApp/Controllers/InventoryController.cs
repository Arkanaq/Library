using Microsoft.AspNetCore.Mvc;
using System.Numerics;
using TestApp.DbModel.Models;
using TestApp.Services;

namespace BookStoreApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InventoryController : ControllerBase
{
    private readonly InventoryService _inventoryService;

    public InventoryController(InventoryService inventoryService) =>
        _inventoryService = inventoryService;

    [HttpGet]
    public async Task<List<Book>> Get() =>
        await _inventoryService.GetAsync();

    [HttpGet("id")]
    public async Task<ActionResult<Book>> Get(Guid id)
    {
        var inventory = await _inventoryService.GetAsync(id);

        if (inventory is null)
        {
            return NotFound();
        }

        return inventory;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Book newInventory)
    {
        await _inventoryService.CreateAsync(newInventory);

        return CreatedAtAction(nameof(Get), new { id = newInventory.Id }, newInventory);
    }

    [HttpPut]
    public async Task<IActionResult> Update(Guid id, Book updatedInventory)
    {
        var book = await _inventoryService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        updatedInventory.Id = book.Id;

        await _inventoryService.UpdateAsync(id, updatedInventory);

        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(Guid id)
    {
        var book = await _inventoryService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        await _inventoryService.RemoveAsync(id);

        return NoContent();
    }
}