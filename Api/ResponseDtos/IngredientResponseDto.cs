using DB.Models;

namespace Api.ResponseModels;

public class IngredientResponseDto
{
    public IngredientResponseDto(Ingredient ingredient)
    {
        Id = ingredient.Id;
        Title = ingredient.Title;
    }
    public int Id { get; set; }
    public string Title { get; set; }
}