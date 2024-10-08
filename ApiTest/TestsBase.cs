using Api.ResponseModels;
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
    
    protected readonly List<Ingredient> BaseIngredients =
    [
        new() { Title = "Potatoes" }, // Roast dinner
        new() { Title = "Chicken" }, // Roast dinner
        new() { Title = "Cheese" }, // Toastie
    ];

    protected readonly List<RecipeGroup> BaseRecipeGroups =
    [
        new() { Title = "High Protein", Description = "A meal that offers a lot of protein" },
        new() { Title = "Vegetarian", Description = "No meat" }
    ];

    public TestsBase(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        var connectionString = "Server=127.0.0.1;Database=RecipesTest;User=root;Password=example";

        var options = new DbContextOptionsBuilder<RecipeDbContext>()
            .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
            .Options;
        _dbContext = new RecipeDbContext(options);
        _dbContext.Recipes.AddRange(BaseRecipes);
        _dbContext.SaveChanges();
    }

    public async Task InsertIngredients(List<Ingredient>? ingredients = null)
    {
        ingredients ??= BaseIngredients;
        
        await _dbContext.Ingredients.AddRangeAsync(ingredients);
        await _dbContext.SaveChangesAsync();
    }
    
    protected async Task<List<RecipeIngredient>> LinkBaseRecipeIngredients()
    {
        await InsertIngredients();
        List<RecipeIngredient> recipeIngredients = new()
        {
            new()
            {
                RecipeId = BaseRecipes[0].Id,
                IngredientId = BaseIngredients[0].Id,
                Comment = "You want to cut them up into small pieces",
                Quantity = 1,
                UnitOfQuantity = UnitsOfMeasurement.Kilograms,
            },
            new()
            {
                RecipeId = BaseRecipes[0].Id,
                IngredientId = BaseIngredients[1].Id,
                Comment = "Salt brining this the night before makes it even better",
                Quantity = 1,
                UnitOfQuantity = UnitsOfMeasurement.NoUnit,
            },
            new()
            {
                RecipeId = BaseRecipes[1].Id,
                IngredientId = BaseIngredients[2].Id,
                Comment = "You can use different cheese with this",
                Quantity = 1,
                UnitOfQuantity = UnitsOfMeasurement.Cups,
            },
        };
        
        await _dbContext.RecipeIngredients.AddRangeAsync(recipeIngredients);
        await _dbContext.SaveChangesAsync();
        return recipeIngredients;
    }
    
    protected async Task<List<RecipeGroupRecipe>> LinkBaseRecipeGroupRecipes()
    {
        await InsertRecipeGroups(BaseRecipeGroups);
        List<RecipeGroupRecipe> recipeGroupRecipes = new()
        {
            new()
            {
                RecipeId = BaseRecipes[0].Id,
                RecipeGroupId = BaseRecipeGroups[0].Id,
            },
            new()
            {
                RecipeId = BaseRecipes[1].Id,
                RecipeGroupId = BaseRecipeGroups[0].Id,
            },
        };
        
        await _dbContext.RecipeGroupRecipes.AddRangeAsync(recipeGroupRecipes);
        await _dbContext.SaveChangesAsync();
        return recipeGroupRecipes;
    }

    protected List<RecipeResponseDto> GetRecipeResponseDtos(List<Recipe> recipes)
    {
        return recipes.Select(r => new RecipeResponseDto(r)).ToList();
    }

    protected async Task<List<RecipeGroup>> InsertRecipeGroups(List<RecipeGroup> recipeGroups)
    {
        await _dbContext.RecipeGroups.AddRangeAsync(recipeGroups);
        await _dbContext.SaveChangesAsync();
        return recipeGroups;
    }

    public async void Dispose()
    {
        await _dbContext.Ingredients.ExecuteDeleteAsync();
        await _dbContext.Recipes.ExecuteDeleteAsync();
        await _dbContext.RecipeIngredients.ExecuteDeleteAsync();
        await _dbContext.RecipeGroups.ExecuteDeleteAsync();
        await _dbContext.RecipeGroupRecipes.ExecuteDeleteAsync();
    }
}