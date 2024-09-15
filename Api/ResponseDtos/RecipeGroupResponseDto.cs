using DB.Models;

namespace Api.ResponseModels;

public class RecipeGroupResponseDto
{
    public RecipeGroupResponseDto(){}
    public RecipeGroupResponseDto(RecipeGroup recipeGroup)
    {
        Id = recipeGroup.Id;
        Title = recipeGroup.Title;
        Description = recipeGroup.Description;
    }
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
}