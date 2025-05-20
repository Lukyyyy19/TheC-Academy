using Microsoft.EntityFrameworkCore;
using Todo_List.Models;
namespace Todo_List.Controllers;

public class TodoDb:DbContext
{
    public TodoDb(DbContextOptions<TodoDb> options):base(options){}
    public DbSet<Todo> Todos => Set<Todo>();
}