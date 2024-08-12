using System.ComponentModel.DataAnnotations;

namespace DB.Models;

public enum Season
{
    Grams,
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
    public int Id { get; set; }
    public int RecipeId { get; set; }

    // public Recipe Recipe { get; set; } = null!;
    public int IngredientId { get; set; }
    public DateTime CreatedTimestamp { get; set; } = DateTime.Now;
    public bool IsDeleted { get; set; }
    [MaxLength(400)]
    public string? Comment { get; set; }
    public int Quantity { get; set; }
    public Season UnitOfQuantity { get; set; }

    // public Ingredient Ingredient { get; set; } = null!;
}