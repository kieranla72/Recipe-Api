using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using ApiTest.Comparers;
using DB.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiTest.Controllers;

[Collection("Sequential")]
public class RecipesControllerTest : TestsBase
{
    private RecipesComparer _recipesComparer;
    public RecipesControllerTest(CustomWebApplicationFactory<Program> factory) : base(factory)
    {
        _recipesComparer = new RecipesComparer();
    }

    [Fact]
    public async Task CreateRecipes()
    {
        var client = _factory.CreateClient();
        var newRecipes = GetNewRecipes();

        var response = await client.PostAsJsonAsync("/Recipes", newRecipes);
        var recipes = await response.Content.ReadFromJsonAsync<List<Recipe>>();
        var sortedRecipes = recipes.OrderBy(ft => ft.Title).ToList();

        newRecipes[0].Id = sortedRecipes[0].Id;
        newRecipes[1].Id = sortedRecipes[1].Id;
        
        var insertedRecipes = await _dbContext.Recipes.ToListAsync();

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Equal(2, newRecipes.Count);
        Assert.Equal(JsonSerializer.Serialize(newRecipes), JsonSerializer.Serialize(recipes));
        var newRecipeIds = new List<int> { newRecipes[0].Id, newRecipes[1].Id };

        var filteredInsertedRecipes = insertedRecipes.Where(i => newRecipeIds.Contains(i.Id)).ToList();
        Assert.True(_recipesComparer.Equals(newRecipes, filteredInsertedRecipes));
    }
    
    [Fact]
    public async Task GetRecipes()
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync("/Recipes");
        var recipes = await response.Content.ReadFromJsonAsync<List<Recipe>>();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(recipes);
        Assert.Equal(2, recipes.Count);
        Assert.Equal(JsonSerializer.Serialize(BaseRecipes), JsonSerializer.Serialize(recipes));
    }
    
    [Fact]
    public async Task GetRecipesWithLinkedIngredients()
    {
        var client = _factory.CreateClient();
        await LinkBaseRecipeIngredients();

        var response = await client.GetAsync("/Recipes");
        var recipes = await response.Content.ReadFromJsonAsync<List<Recipe>>();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(recipes);
        Assert.Equal(2, recipes.Count);
        JsonSerializerOptions jsonOptions = new()
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            
        };
        Assert.True(_recipesComparer.Equals(BaseRecipes, recipes));
    }

    private List<Recipe> GetNewRecipes()
    {
        return
        [
            new() { Title = "Jerk Chicken", Description = "A Spicy chicken and rice meal", CookingTimeInMinutes = 60,},
            new() { Title = "Milanese Risotto", CookingTimeInMinutes = 40,},
        ];
    }
}