using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Api.ResponseModels;
using ApiTest.Comparers;
using DB.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiTest.Controllers;

[Collection("Sequential")]
public class RecipesControllerTest : TestsBase
{
    private RecipesComparer _recipesComparer;
    private RecipeResponseDtosComparer _recipeResponseDtosComparer;
    public RecipesControllerTest(CustomWebApplicationFactory<Program> factory) : base(factory)
    {
        _recipesComparer = new RecipesComparer();
        _recipeResponseDtosComparer = new RecipeResponseDtosComparer();
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
    public async Task CreateRecipes_WithLinkedIngredients()
    {
        var client = _factory.CreateClient();
        var newRecipes = await GetNewRecipesWithLinkedIngredients();

        var response = await client.PostAsJsonAsync("/Recipes", newRecipes);
        var recipes = await response.Content.ReadFromJsonAsync<List<Recipe>>();
        var sortedRecipes = recipes.OrderBy(ft => ft.Title).ToList();
        
        
        // We need to replace all of the default IDs with auto-generated incrementing IDs
        newRecipes[0].Id = sortedRecipes[0].Id;
        newRecipes[0].RecipeIngredients[0].Id = sortedRecipes[0].RecipeIngredients[0].Id;
        newRecipes[0].RecipeIngredients[0].RecipeId = sortedRecipes[0].RecipeIngredients[0].RecipeId;
        newRecipes[0].RecipeIngredients[1].Id = sortedRecipes[0].RecipeIngredients[1].Id;
        newRecipes[0].RecipeIngredients[1].RecipeId = sortedRecipes[0].RecipeIngredients[1].RecipeId;
        
        newRecipes[1].Id = sortedRecipes[1].Id;
        newRecipes[1].RecipeIngredients[0].Id = sortedRecipes[1].RecipeIngredients[0].Id;
        newRecipes[1].RecipeIngredients[0].RecipeId = sortedRecipes[1].RecipeIngredients[0].RecipeId;
        
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
        var recipes = await response.Content.ReadFromJsonAsync<List<RecipeResponseDto>>();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(recipes);
        Assert.Equal(2, recipes.Count);
        Assert.Equal(JsonSerializer.Serialize(GetRecipeResponseDtos(BaseRecipes)), JsonSerializer.Serialize(recipes));
    }
    
    [Fact]
    public async Task GetRecipesWithLinkedIngredients()
    {
        var client = _factory.CreateClient();
        await LinkBaseRecipeIngredients();

        var response = await client.GetAsync("/Recipes");
        var recipes = await response.Content.ReadFromJsonAsync<List<RecipeResponseDto>>();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(recipes);
        Assert.Equal(2, recipes.Count);
        Assert.True(_recipeResponseDtosComparer.Equals(GetRecipeResponseDtos(BaseRecipes), recipes));
    }

    private List<Recipe> GetNewRecipes()
    {
        return
        [
            new() { Title = "Jerk Chicken", Description = "A Spicy chicken and rice meal", CookingTimeInMinutes = 60,},
            new() { Title = "Milanese Risotto", CookingTimeInMinutes = 40,},
        ];
    }
    
    private async Task<List<Recipe>> GetNewRecipesWithLinkedIngredients()
    {
        List<Ingredient> ingredients = new()
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
                Title = "Risotto Rice"
            },
        };

        await InsertIngredients(ingredients);

        List<RecipeIngredient> recipeIngredients = new()
        {
            new()
            {
                IngredientId = ingredients[0].Id,
                Comment = "comment0",
                Quantity = 5,
                UnitOfQuantity = UnitsOfMeasurement.NoUnit,
            },
            new()
            {
                IngredientId = ingredients[1].Id,
                Comment = "comment1",
                Quantity = 400,
                UnitOfQuantity = UnitsOfMeasurement.Grams,
            },
            new()
            {
                IngredientId = ingredients[2].Id,
                Comment = "comment2",
                Quantity = 1,
                UnitOfQuantity = UnitsOfMeasurement.Cups,
            },
        };

        return
        [
            new()
            {
                Title = "Jerk Chicken", Description = "A Spicy chicken and rice meal", CookingTimeInMinutes = 60,
                RecipeIngredients = new List<RecipeIngredient> { recipeIngredients[0], recipeIngredients[1] },
                Ingredients = new List<Ingredient> { ingredients[0], ingredients[1] }
            },
            new()
            {
                Title = "Milanese Risotto", CookingTimeInMinutes = 40,
                RecipeIngredients = new List<RecipeIngredient> { recipeIngredients[2] },
                Ingredients = new List<Ingredient> { ingredients[2] }

            },
        ];
    }
}