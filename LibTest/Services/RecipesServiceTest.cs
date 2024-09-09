using DB.Daos;
using DB.Models;
using Lib.Exceptions;
using Lib.Services;
using Moq;

namespace LibTest.Services;

public class RecipeServiceTest : TestsBase
{
    
    private readonly Mock<IRecipesDao> _recipesDaoMock =  new ();
    private RecipeService SetUpService()
    {
        return new RecipeService(_recipesDaoMock.Object);
    }

    [Fact]
    public async Task GetRecipeById_NotFound()
    {
        var recipesService = SetUpService();

        // No need to mock _recipesDaoMock as Mock<> always returns null anyway
        await Assert.ThrowsAsync<NotFoundException>(() => recipesService.GetRecipeById(1));
    }
    
    [Fact]
    public async Task GetRecipeById_ReturnsRecipeWhenFound()
    {
        var recipesService = SetUpService();
        var mockRecipe = GetRecipes()[0];

        _recipesDaoMock
            .Setup(i => i.GetRecipeById(mockRecipe.Id))
            .ReturnsAsync(mockRecipe);

        var recipe = await recipesService.GetRecipeById(mockRecipe.Id);
        Assert.Equal(mockRecipe, recipe);
    }
}