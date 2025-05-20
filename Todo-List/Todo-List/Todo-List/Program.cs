using Microsoft.EntityFrameworkCore;
using Todo_List.Models;
using Todo_List.Controllers;
using Todo_List.EndPoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TodoDb>(opt => opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();
app.UseDefaultFiles(); // Serve index.html when navigating to "/"
app.UseStaticFiles(); // Enable wwwroot files
app.MapEndpoints();

app.Run();

