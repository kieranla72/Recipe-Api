using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Api.InputDtos;
using ApiTest.Comparers;
using DB;
using DB.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiTest.Controllers;

[Collection("Sequential")]
public class IngredientsControllerTest : TestsBase
{
    private IngredientsComparer _ingredientsComparer;

    public IngredientsControllerTest(CustomWebApplicationFactory<Program> factory) : base(factory)
    {
        _ingredientsComparer = new IngredientsComparer();
    }

    [Fact]
    public async Task CreateIngredients()
    {
        var client = _factory.CreateClient();
        var newIngredients = GetNewIngredientsList();
        
        var response = await client.PostAsJsonAsync("/Ingredients", newIngredients);
        var ingredients = await response.Content.ReadFromJsonAsync<List<Ingredient>>();
        var sortedIngredients = ingredients.OrderBy(g => g.Title).ToList();
        newIngredients[0].Id = sortedIngredients[0].Id;
        newIngredients[1].Id = sortedIngredients[1].Id;
        newIngredients[2].Id = sortedIngredients[2].Id;

        var insertedIngredients = await _dbContext.Ingredients.ToListAsync();
        
        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Equal(3, ingredients.Count);
        Assert.True(_ingredientsComparer.Equals(newIngredients, sortedIngredients));
        Assert.True(_ingredientsComparer.Equals(newIngredients, insertedIngredients));
    }
    
    [Fact]
    public async Task GetIngredients()
    {
        // Arrange
        var ingredientsToAdd = await SaveIngredientsToDb();
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/Ingredients");

        // Assert
        response.EnsureSuccessStatusCode();
        var ingredients = await response.Content.ReadFromJsonAsync<List<Ingredient>>();
        Assert.True(_ingredientsComparer.Equals(ingredientsToAdd, ingredients));
    }    
    
    [Fact]
    public async Task GetIngredientById()
    {
        // Arrange
        var ingredientsToAdd = await SaveIngredientsToDb();
        var client = _factory.CreateClient();

        // Act  
        var response = await client.GetAsync($"/Ingredients/{ingredientsToAdd[0].Id}");

        // Assert
        response.EnsureSuccessStatusCode();
        var ingredient = await response.Content.ReadFromJsonAsync<Ingredient>();
        Assert.True(_ingredientsComparer.Equals(ingredientsToAdd[0], ingredient));
    }    
    [Fact]
    public async Task GetIngredientById_NotFound()
    {
        // Arrange
        await SaveIngredientsToDb();
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/Ingredients/123123");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
    
        
    [Theory]
    [InlineData("Chicken", 1)]
    [InlineData("rice", 2)]
    [InlineData("any string", 0)]
    public async void SearchIngredients(string title, int expectedResult)
    {
        // Arrange
        var client = _factory.CreateClient();
        await SaveIngredientsToDb();
        await SaveIngredientsToDb(new() { new() { Title = "Brown rice" } }); // Add extra ingredient to show searching brings back multiple

        // Act
        IngredientsSearchDto searchDto = new () { Title = title };
        var response =
            await client.PostAsJsonAsync("/Ingredients/Search", searchDto);

        // Assert
        response.EnsureSuccessStatusCode();
        var ingredients = await response.Content.ReadFromJsonAsync<List<Ingredient>>();
        Assert.Equal(expectedResult, ingredients?.Count);
    }    
    
    private async Task<List<Ingredient>> SaveIngredientsToDb(List<Ingredient>? ingredients = null)
    {
        var ingredientsToAdd = ingredients ?? GetNewIngredientsList();
        _dbContext.Ingredients.AddRange(ingredientsToAdd);
        await _dbContext.SaveChangesAsync();
        return ingredientsToAdd;
    }


    private List<Ingredient> GetNewIngredientsList()
    {
        return new()
        {
            new()
            {
                Title = "Chicken Thighs"
            },
            new()
            {
                Title = "Kidney Beans"
            },
            new()
            {
                Title = "White Rice"
            },
        };
    }
}