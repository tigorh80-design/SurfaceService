using BusinessLayer;

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

// Register ForumPostService for DI
builder.Services.AddScoped<IForumPostService, ForumPostService>();

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