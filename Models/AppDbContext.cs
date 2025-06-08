using Microsoft.EntityFrameworkCore;
using MyEmptyMvcApp.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<TodoItem> TodoItems { get; set; }
}
