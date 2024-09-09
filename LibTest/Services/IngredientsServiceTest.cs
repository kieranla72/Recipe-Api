using DB.Daos;
using DB.Models;
using Lib.Exceptions;
using Lib.Services;
using Moq;

namespace LibTest.Services;

public class IngredientsServiceTest : TestsBase
{
    
    private readonly Mock<IIngredientsDao> _ingredientsDaoMock =  new ();
    private IngredientsService SetUpService()
    {
        return new IngredientsService(_ingredientsDaoMock.Object);
    }

    [Fact]
    public async Task GetIngredientById_NotFound()
    {
        var ingredientsService = SetUpService();

        // No need to mock _ingredientsDaoMock as Mock<> always returns null anyway
        await Assert.ThrowsAsync<NotFoundException>(() => ingredientsService.GetIngredientById(1));
    }
    
    [Fact]
    public async Task GetIngredientById_ReturnsIngredientWhenFound()
    {
        var ingredientsService = SetUpService();
        var mockIngredient = GetIngredients()[0];

        _ingredientsDaoMock
            .Setup(i => i.GetIngredientById(mockIngredient.Id))
            .ReturnsAsync(mockIngredient);

        var ingredient = await ingredientsService.GetIngredientById(mockIngredient.Id);
        Assert.Equal(mockIngredient, ingredient);
    }
}