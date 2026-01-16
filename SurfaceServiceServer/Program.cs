using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.EF;
using RepositoryLayer.Repositories;
using System;

var builder = WebApplication.CreateBuilder(args);

// Early logger for diagnostics
//using var loggerFactory = LoggerFactory.Create(lb => lb.AddConsole());
//var logger = loggerFactory.CreateLogger("Startup");

//logger.LogInformation("ASPNETCORE_ENVIRONMENT = {Env}", builder.Environment.EnvironmentName);
//logger.LogInformation("Resolved DefaultConnection: {Conn}", builder.Configuration.GetConnectionString("DefaultConnection"));


// Add services to the container.

builder.Services.AddControllers();

//Cors policy to allow React app access
builder.Services.AddCors(options => {
    options.AddPolicy("AllowReactApp",
    policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
            //.AllowCredentials(); // Uncomment if you need cookies/auth
    });
});

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
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
           .EnableDetailedErrors()      // dev only
           .EnableSensitiveDataLogging() // dev only
);

// DI Registertrations
builder.Services.AddScoped<IForumPostService, ForumPostService>();
builder.Services.AddScoped<IShotCallerService, ShotCallerService>();
builder.Services.AddScoped<IShotCallerRepository, ShotCallerRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Surface Service V1");
});

app.UseHttpsRedirection();

app.UseAuthorization();

// Must enable CORS before routing/controllers handling
app.UseCors("AllowReactApp");

app.MapControllers();

app.Run();