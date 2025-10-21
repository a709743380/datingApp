using API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("Default"));
});
builder.Services.AddCors();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

var app = builder.Build();

app.UseCors(
        options =>
             options
             .AllowAnyHeader()
             .AllowAnyMethod()
             .WithOrigins("http://localhost:4200", "https://localhost:4200")
    );
app.UseAuthorization();

app.MapControllers();

app.Run();
