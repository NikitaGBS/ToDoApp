using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Data;
using ToDoApp.Data.Entities;
using ToDoApp.Messaging;
using ToDoApp.Messaging.Models;

namespace ToDoApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ToDoController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly KafkaProducer _kafkaProducer;
    public ToDoController(AppDbContext context, KafkaProducer kafkaProducer)
    {
        _context = context;
        _kafkaProducer = kafkaProducer;
    }

    [HttpGet("list")]
    public async Task<IEnumerable<TodoItem>> Get() =>
        await _context.TodoItems.ToListAsync();
    
    [HttpPost("add")]
    public async Task<ActionResult<TodoItem>> Add([FromBody] TodoItem item)
    {
        _context.TodoItems.Add(item);
        await _context.SaveChangesAsync();

        await _kafkaProducer.ProduceAsync(KafkaTopics.TodoCreated, new TodoCreatedEvent
        {
            DateTime = item.DateTime,
            Id = item.Id,
            IsCompleted = item.IsCompleted,
            Title = item.Title
        });
        
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