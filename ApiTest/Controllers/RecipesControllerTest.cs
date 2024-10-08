using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
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
        newRecipes[0].Ingredients = new List<Ingredient>();
        
        newRecipes[1].Id = sortedRecipes[1].Id;
        newRecipes[1].RecipeIngredients[0].Id = sortedRecipes[1].RecipeIngredients[0].Id;
        newRecipes[1].RecipeIngredients[0].RecipeId = sortedRecipes[1].RecipeIngredients[0].RecipeId;
        newRecipes[1].Ingredients = new List<Ingredient>();
        
        var insertedRecipes = await _dbContext.Recipes.ToListAsync();

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Equal(2, newRecipes.Count);
        Assert.Equal(JsonSerializer.Serialize(newRecipes), JsonSerializer.Serialize(recipes));
        var newRecipeTitles = new List<string> { newRecipes[0].Title, newRecipes[1].Title };

        var filteredInsertedRecipes = insertedRecipes.Where(i => newRecipeTitles.Contains(i.Title)).ToList();
        // Set the ids on the mock new recipes
        for (var i = 0; i < newRecipes.Count; i++)
        {
            newRecipes[i] = filteredInsertedRecipes[i];
        }
        
        Assert.True(_recipesComparer.Equals(newRecipes, filteredInsertedRecipes));
    }
    
    
    [Fact]
    public async Task UpdateRecipe()
    {
        var client = _factory.CreateClient();
        var newRecipe = BaseRecipes[0];
        var newRecipeTitle = "This is a new recipe title";
        newRecipe.Title = newRecipeTitle;

        var response = await client.PutAsJsonAsync("/Recipes", newRecipe);
        var recipe = await response.Content.ReadFromJsonAsync<Recipe>();

        var insertedRecipe = await _dbContext.Recipes.FindAsync(newRecipe.Id);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Equal(newRecipeTitle, recipe.Title);
        Assert.Equal(newRecipeTitle, insertedRecipe.Title);
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
    
    [Fact]
    public async Task GetRecipeById()
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync($"/Recipes/{BaseRecipes[0].Id}");
        var recipe = await response.Content.ReadFromJsonAsync<RecipeResponseDto>();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(recipe);
        Assert.True(_recipeResponseDtosComparer.Equals(GetRecipeResponseDtos(BaseRecipes)[0], recipe));
    }
    
    [Fact]
    public async Task GetRecipeById_NotFoundStatusCode()
    {
        var client = _factory.CreateClient();
        var idWithNoCorrespondingRecipes = 123123123;

        var response = await client.GetAsync($"/Recipes/{idWithNoCorrespondingRecipes}");
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetRecipesByRecipeGroup()
    {
        var client = _factory.CreateClient();
        var recipeGroupRecipes = await LinkBaseRecipeGroupRecipes();
        var recipeGroupId = recipeGroupRecipes[0].RecipeGroupId;
        
        var response = await client.GetAsync($"/Recipes/RecipeGroups/{recipeGroupId}");
        response.EnsureSuccessStatusCode();
        var recipes = await response.Content.ReadFromJsonAsync<List<Recipe>>();
        
        Assert.Equal(2, recipes.Count);
        Assert.Equal(recipeGroupRecipes[0].RecipeId, recipes[0].Id);
        Assert.Equal(recipeGroupRecipes[1].RecipeId, recipes[1].Id);
    }
    
    [Fact]
    public async Task GetRecipesByRecipeGroup_RecipeGroupWithNoRecipesReturnsOk()
    {
        var client = _factory.CreateClient();
        var recipeGroupWithNoRecipes = await InsertRecipeGroups([BaseRecipeGroups[0]]);
        var recipeGroupId = recipeGroupWithNoRecipes[0].Id;
        
        var response = await client.GetAsync($"/Recipes/RecipeGroups/{recipeGroupId}");
        response.EnsureSuccessStatusCode();
        var recipes = await response.Content.ReadFromJsonAsync<List<Recipe>>();

        Assert.Empty(recipes);
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