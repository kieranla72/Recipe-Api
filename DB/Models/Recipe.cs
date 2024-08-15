using System.ComponentModel.DataAnnotations;

namespace DB.Models;

public class Recipe
{
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }

    public string? Description { get; set; }
    [Required]
    public int CookingTimeInMinutes { get; set; }

    public List<Ingredient> Ingredients { get; set; } = [];
    public List<RecipeIngredient> RecipeIngredients { get; set; } = [];
}