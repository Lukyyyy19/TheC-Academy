using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MVCMovies.Data;
using MVCMovies.Models;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MVCMoviesContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MVCMoviesContext") ?? throw new InvalidOperationException("Connection string 'MVCMoviesContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var servicies = scope.ServiceProvider;
    SeedData.Initialize(servicies);
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=HelloWorld}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();