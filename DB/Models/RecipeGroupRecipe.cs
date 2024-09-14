namespace DB.Models;

public class RecipeGroupRecipe
{
    public int Id { get; init;  }
    public int RecipeGroupId { get; init; }
    public int RecipeId { get; init; }
}