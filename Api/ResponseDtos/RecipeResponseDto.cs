using System.ComponentModel.DataAnnotations;
using DB.Models;

namespace Api.ResponseModels;

public class RecipeResponseDto
{
    public RecipeResponseDto(){}
    public RecipeResponseDto(Recipe recipe)
    {
        Id = recipe.Id;
        Title = recipe.Title;
        Description = recipe.Description;
        CookingTimeInMinutes = recipe.CookingTimeInMinutes;
        Ingredients = GetIngredientsResponseDto(recipe.Ingredients, recipe.RecipeIngredients);
    }

    private List<RecipeIngredientsResponseDto> GetIngredientsResponseDto(List<Ingredient> ingredients, List<RecipeIngredient> recipeIngredients)
    {
        var ingredientIdToIngredientDict = ingredients.ToDictionary(i => i.Id, i => i);
        var ingredientsResponseDtos =
            recipeIngredients.Select(ri => new RecipeIngredientsResponseDto(ingredientIdToIngredientDict[ri.IngredientId], ri))
                .ToList();
        return ingredientsResponseDtos;
    }

    public int Id { get; init; }
    public string Title { get; init; }
    public string? Description { get; init; }
    public int CookingTimeInMinutes { get; init; }
    public List<RecipeIngredientsResponseDto> Ingredients { get; init; }
}

public class RecipeIngredientsResponseDto
{
    public RecipeIngredientsResponseDto() {}
    public RecipeIngredientsResponseDto(Ingredient ingredient, RecipeIngredient recipeIngredient)
    {
        Id = ingredient.Id;
        Title = ingredient.Title;
        CreatedTimestamp = recipeIngredient.CreatedTimestamp;
        Comment = recipeIngredient.Comment;
        Quantity = recipeIngredient.Quantity;
        UnitOfQuantity = recipeIngredient.UnitOfQuantity;
    }
    public int Id { get; init; }
    public string Title { get; init; }
    public DateTime CreatedTimestamp { get; init; } = DateTime.Now;
    [MaxLength(400)]
    public string? Comment { get; init; }
    public int Quantity { get; init; }
    public UnitsOfMeasurement UnitOfQuantity { get; init; }
}

