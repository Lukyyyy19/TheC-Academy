using MyToDo_List.Models;
using MyToDo_List.Data;
using Microsoft.EntityFrameworkCore;
namespace MyToDo_List.Endpoints;

public static class Endpoints
{
    public static WebApplication MapEndpoints(this WebApplication app)
    {
        var todoItems = app.MapGroup("/todoItems");
        todoItems.MapGet("/", GetAllTodos);
        todoItems.MapGet("/complete", GetCompleteTodos);
        todoItems.MapGet("/{id}", GetTodo);
        todoItems.MapPost("/", CreateTodo);
        todoItems.MapPut("/{id}", UpdateTodo);
        todoItems.MapDelete("/{id}", DeleteTodo);
        return app;
    }
    static async Task<IResult> GetAllTodos(TodoDbContext db)
    {
        return TypedResults.Ok(await db.Todos.Select(x=> new TodoItemDTO(x)).ToArrayAsync());
    }
    
    static async Task<IResult> GetCompleteTodos(TodoDbContext db)
    {
        return TypedResults.Ok(await db.Todos.Where(t => t.IsComplete).Select(x=> new TodoItemDTO(x)).ToListAsync());
    }
    
    static async Task<IResult> GetTodo(int id, TodoDbContext db)
    {
        return await db.Todos.FindAsync(id)
            is Todo todo
            ? TypedResults.Ok(new TodoItemDTO(todo))
            : TypedResults.NotFound();
    }
    
    static async Task<IResult> CreateTodo(TodoItemDTO todoItemDTO, TodoDbContext db)
    {
        var todoItem = new Todo
        {
            IsComplete = todoItemDTO.IsComplete,
            Name = todoItemDTO.Name
        };
        db.Todos.Add(todoItem);
        await db.SaveChangesAsync();
    
        return TypedResults.Created($"/todoitems/{todoItem.Id}", todoItemDTO);
    }
    
    static async Task<IResult> UpdateTodo(int id, TodoItemDTO inputTodo, TodoDbContext db)
    {
        var todo = await db.Todos.FindAsync(id);
    
        if (todo is null) return TypedResults.NotFound();
    
        todo.Name = inputTodo.Name;
        todo.IsComplete = inputTodo.IsComplete;
    
        await db.SaveChangesAsync();
    
        return TypedResults.NoContent();
    }
    
    static async Task<IResult> DeleteTodo(int id, TodoDbContext db)
    {
        if (await db.Todos.FindAsync(id) is Todo todo)
        {
            db.Todos.Remove(todo);
            await db.SaveChangesAsync();
            return TypedResults.NoContent();
        }
    
        return TypedResults.NotFound();
    }
}