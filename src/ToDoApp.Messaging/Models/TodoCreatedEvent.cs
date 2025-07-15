namespace ToDoApp.Messaging.Models;

public class TodoCreatedEvent
{
    public DateTime DateTime { get; set; }
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public bool IsCompleted { get; set; }
}