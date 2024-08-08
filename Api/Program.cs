using DB;
using DB.Daos;
using Lib.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddTransient<IIngredientsDao, IngredientsDao>();
builder.Services.AddTransient<IIngredientsService, IngredientsService>();
builder.Services.AddTransient<IRecipesDao, RecipesDao>();
builder.Services.AddTransient<IRecipeService, RecipeService>();
builder.Services.AddSingleton<ICacheManagerService, CacheManagerService>();

builder.Services.AddMemoryCache();

var connectionString = builder.Configuration.GetConnectionString("RecipeDbContextConnection");
builder.Services.AddDbContext<RecipeDbContext>(options => 
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21))));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Ensure migrations are applied during startup
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var dbContext = services.GetRequiredService<RecipeDbContext>();
        dbContext.Database.Migrate();
        // Optionally, you can seed initial data here if needed
        // EnsureSeedData(dbContext);
    }
    catch (Exception ex)
    {
        // Log the exception or handle it accordingly
        Console.WriteLine($"An error occurred while migrating the database: {ex.Message}");
        throw;
    }
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
public partial class Program { }