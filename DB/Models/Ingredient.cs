namespace DB.Models;

public class Ingredient
{
    public int Id { get; set; }
    public string Title { get; init; }
    
    public List<Recipe> Recipes { get; } = [];
}