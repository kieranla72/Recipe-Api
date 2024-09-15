
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using DB.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiTest.Controllers;

[Collection("Sequential")]
public class RecipeGroupsControllerTest : TestsBase
{
    public RecipeGroupsControllerTest(CustomWebApplicationFactory<Program> factory) : base(factory) {}

    [Fact]
    public async Task CreateRecipeGroup()
    {
        var client = _factory.CreateClient();
        var recipeGroup = BaseRecipeGroups[0];

        var response = await client.PostAsJsonAsync("/RecipeGroups", recipeGroup);
        var recipeGroupResponse = await response.Content.ReadFromJsonAsync<RecipeGroup>();

        recipeGroup.Id = recipeGroupResponse.Id;

        var insertedRecipeGroup = await _dbContext.RecipeGroups.FindAsync(recipeGroup.Id);
        
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Equal(JsonSerializer.Serialize(recipeGroup), JsonSerializer.Serialize(insertedRecipeGroup));
    }
    
    [Fact]
    public async Task CreateRecipeGroupRecipe()
    {
        var client = _factory.CreateClient();
        var recipeGroup = (await InsertRecipeGroups(BaseRecipeGroups))[0];
        var recipeId = BaseRecipes[0].Id;

        var response = await client.PostAsJsonAsync($"/RecipeGroups/{recipeGroup.Id}",
            recipeId);
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        var insertedRecipeGroup = await _dbContext.RecipeGroupRecipes.SingleAsync(rgr =>
            rgr.RecipeId == recipeId && rgr.RecipeGroupId == recipeGroup.Id);

        Assert.NotNull(insertedRecipeGroup);
        Assert.IsType<RecipeGroupRecipe>(insertedRecipeGroup);
    }

}