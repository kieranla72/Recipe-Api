using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using DB.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiTest.Controllers;

[Collection("Sequential")]
public class RecipesControllerTest : TestsBase
{
    public RecipesControllerTest(CustomWebApplicationFactory<Program> factory) : base(factory)
    {
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
        
        var insertedIngredients = await _dbContext.Recipes.ToListAsync();

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Equal(2, newRecipes.Count);
        Assert.Equal(JsonSerializer.Serialize(newRecipes), JsonSerializer.Serialize(recipes));
        var newRecipeIds = new List<int> { newRecipes[0].Id, newRecipes[1].Id };
        Assert.Equal(JsonSerializer.Serialize(newRecipes),
            JsonSerializer.Serialize(insertedIngredients.Where(i => newRecipeIds.Contains(i.Id))));

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