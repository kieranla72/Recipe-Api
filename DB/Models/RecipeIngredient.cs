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
    public int IngredientId { get; set; }
    public DateTime CreatedTimestamp { get; set; } = DateTime.Now;
    public bool IsDeleted { get; set; }
    [MaxLength(400)]
    public string? Comment { get; set; }
    public int Quantity { get; set; }
    public UnitsOfMeasurement UnitOfQuantity { get; set; }
}