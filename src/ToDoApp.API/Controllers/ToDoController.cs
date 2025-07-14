using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Data;
using ToDoApp.Data.Entities;

namespace ToDoApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ToDoController : ControllerBase
{
    private readonly AppDbContext _context;

    public ToDoController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("list")]
    public async Task<IEnumerable<TodoItem>> Get() =>
        await _context.TodoItems.ToListAsync();
    
    [HttpPost("add")]
    public async Task<ActionResult<TodoItem>> Add([FromBody] TodoItem item)
    {
        _context.TodoItems.Add(item);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<TodoItem>> GetById(int id)
    {
        var item = await _context.TodoItems.FindAsync(id);
        if (item == null)
            return NotFound();
        return item;
    }
}