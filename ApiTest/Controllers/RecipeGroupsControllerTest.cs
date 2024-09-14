
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using DB.Models;

namespace ApiTest.Controllers;

[Collection("Sequential")]
public class RecipeGroupsControllerTest : TestsBase
{
    public RecipeGroupsControllerTest(CustomWebApplicationFactory<Program> factory) : base(factory) {}

    [Fact]
    public async Task CreateRecipeGroup()
    {
        var client = _factory.CreateClient();
        var recipeGroup = RecipeGroups[0];

        var response = await client.PostAsJsonAsync("/RecipeGroups", recipeGroup);
        var recipeGroupResponse = await response.Content.ReadFromJsonAsync<RecipeGroup>();

        recipeGroup.Id = recipeGroupResponse.Id;

        var insertedRecipeGroup = await _dbContext.RecipeGroups.FindAsync(recipeGroup.Id);
        
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Equal(JsonSerializer.Serialize(recipeGroup), JsonSerializer.Serialize(insertedRecipeGroup));
    }

}