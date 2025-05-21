using Microsoft.EntityFrameworkCore;
using MyToDo_List.Models;
namespace MyToDo_List.Data;

public class TodoDbContext:DbContext
{
    public TodoDbContext(DbContextOptions<TodoDbContext> options):base(options){}
    public DbSet<Todo> Todos => Set<Todo>();
}