using System.ComponentModel.DataAnnotations;

namespace DB.Models;

public enum UnitsOfMeasurement
{
    Grams,
    Kilograms,
    Pounds,
    Ounces,
    Cups,
    NoUnit,
    Teaspoons,
    Tablespoons,
    Millilitre,
}

public class RecipeIngredient
{
    public RecipeIngredient() {}
    public RecipeIngredient(RecipeIngredient recipeIngredient)
    {
        Id = recipeIngredient.Id;
        RecipeId = recipeIngredient.RecipeId;
        IngredientId = recipeIngredient.IngredientId;
        CreatedTimestamp = recipeIngredient.CreatedTimestamp;
        IsDeleted = recipeIngredient.IsDeleted;
        Comment = recipeIngredient.Comment;
        Quantity = recipeIngredient.Quantity;
        UnitOfQuantity = recipeIngredient.UnitOfQuantity;
    }
    
    public RecipeIngredient(RecipeIngredient recipeIngredient, int newRecipeId)
    {
        Id = recipeIngredient.Id;
        RecipeId = newRecipeId;
        IngredientId = recipeIngredient.IngredientId;
        CreatedTimestamp = recipeIngredient.CreatedTimestamp;
        IsDeleted = recipeIngredient.IsDeleted;
        Comment = recipeIngredient.Comment;
        Quantity = recipeIngredient.Quantity;
        UnitOfQuantity = recipeIngredient.UnitOfQuantity;
    }
    
    public int Id { get; set; }
    public int RecipeId { get; set; }
    public int IngredientId { get; init; }
    public DateTime CreatedTimestamp { get; init; } = DateTime.Now;
    public bool IsDeleted { get; init; }
    [MaxLength(400)]
    public string? Comment { get; init; }
    public int Quantity { get; init; }
    public UnitsOfMeasurement UnitOfQuantity { get; init; }
}