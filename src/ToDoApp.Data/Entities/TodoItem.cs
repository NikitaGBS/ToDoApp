namespace ToDoApp.Data.Entities;

public class TodoItem
{
    public DateTime DateTime { get; set; }
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public bool IsCompleted { get; set; }
}