using Microsoft.EntityFrameworkCore;
using ToDoApp.Data.Entities;

namespace ToDoApp.Data;

public class AppDbContext : DbContext
{
    public DbSet<TodoItem> TodoItems { get; set; } = null!;

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TodoItem>(entity =>
        {
            entity.ToTable("todo_items");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
        });
    }
}