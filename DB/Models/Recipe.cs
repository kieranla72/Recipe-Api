using System.ComponentModel.DataAnnotations;

namespace DB.Models;

public class Recipe
{
    public Recipe()
    {
    }

    // Constructor which removes ingredients as a related entity - This is inserted separately
    public Recipe(Recipe recipeWithIngredients)
    {
        Id = recipeWithIngredients.Id;
        Title = recipeWithIngredients.Title;
        Description = recipeWithIngredients.Description;
        CookingTimeInMinutes = recipeWithIngredients.CookingTimeInMinutes;
        RecipeIngredients = recipeWithIngredients.RecipeIngredients;
    }

    public int Id { get; set; }
    [Required]
    public string Title { get; set; }

    public string? Description { get; set; }
    [Required]
    public int CookingTimeInMinutes { get; set; }

    public List<Ingredient> Ingredients { get; set; } = [];
    public List<RecipeIngredient> RecipeIngredients { get; set; } = [];
    
}