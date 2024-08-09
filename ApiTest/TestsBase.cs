using DB;
using DB.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;

namespace ApiTest;

public class TestsBase : IClassFixture<CustomWebApplicationFactory<Program>>, IDisposable
{
    protected readonly CustomWebApplicationFactory<Program> _factory;
    protected RecipeDbContext _dbContext;

    protected readonly List<Recipe> BaseRecipes =
    [
        new() { Title = "Roast Dinner", CookingTimeInMinutes = 120, Description = "A Beautifully cooked roast dinner"},
        new() { Title = "Toastie", CookingTimeInMinutes = 20, Description = "A cheesy hot mess"},
    ];

    public TestsBase(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        var connectionString = "Server=127.0.0.1;Database=RecipesTest;User=root;Password=example";

        var options = new DbContextOptionsBuilder<RecipeDbContext>()
            .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
            .Options;
        _dbContext = new RecipeDbContext(options);
        // _dbContext.Database.Migrate();
        _dbContext.Recipes.AddRange(BaseRecipes);
        _dbContext.SaveChanges();
    }

    public async void Dispose()
    {
        await _dbContext.Ingredients.ExecuteDeleteAsync();
        await _dbContext.Recipes.ExecuteDeleteAsync();
    }
}