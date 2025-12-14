using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.EF;
using RepositoryLayer.Repositories;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register HttpClient for JSONPlaceholder API
builder.Services.AddHttpClient("JSONPlaceholder", client =>
{
    client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
});

// Register EF Core DbContext
builder.Services.AddDbContext<RepoDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// DI Registertrations
builder.Services.AddScoped<IForumPostService, ForumPostService>();
builder.Services.AddScoped<IPostRepository, PostRepository>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Surface Service V1");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();