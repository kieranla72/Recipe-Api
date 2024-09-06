
using DB.Models;

namespace Api.InputDtos;

public class RecipeCreateDto
{
    public string Title { get; init; }
    public string? Description { get; init; }
    public int CookingTimeInMinutes { get; init; }
    public List<RelatedIngredientDto> RelatedIngredients { get; init; }
}

public class RelatedIngredientDto
{
    public int IngredientId { get; set; }
    public string? Comment { get; init; }
    public int Quantity { get; init; }
    public UnitsOfMeasurement UnitOfQuantity { get; init; }
}