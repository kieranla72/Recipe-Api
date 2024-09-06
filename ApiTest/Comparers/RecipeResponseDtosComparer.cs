using Api.ResponseModels;
using DB.Models;

namespace ApiTest.Comparers;

public class RecipeResponseDtosComparer : IEqualityComparer<List<RecipeResponseDto>>
{
    private IngredientResponseDtosComparer _ingredientsComparer;
    public RecipeResponseDtosComparer()
    {
        _ingredientsComparer = new IngredientResponseDtosComparer();
    }
    
    public bool Equals(List<RecipeResponseDto>? list1, List<RecipeResponseDto>? list2)
    {
        if (ReferenceEquals(list1, list2))
            return true;

        if (list1 == null || list2 == null)
            return false;

        if (list1.Count != list2.Count)
            return false;

        var orderedList1 = list1.OrderByDescending(g => g.Id).ToList();
        var orderedList2 = list2.OrderByDescending(g => g.Id).ToList();

        // Check each Recipe in both lists
        for (int i = 0; i < list1.Count; i++)
        {
            if (!RecipesAreEqual(orderedList1[i], orderedList2[i]))
                return false;
        }

        return true;
    }

    public bool Equals(RecipeResponseDto? recipe1, RecipeResponseDto? recipe2)
    {
        if (ReferenceEquals(recipe1, recipe2))
            return true;

        if (recipe1 == null || recipe2 == null)
            return false;

        return RecipesAreEqual(recipe1, recipe2);
    }

    public int GetHashCode(List<RecipeResponseDto> list)
    {
        unchecked
        {
            int hash = 17;
            foreach (var recipe in list)
            {
                hash = hash * 23 + GetRecipeHashCode(recipe);
            }
            return hash;
        }
    }

    private bool RecipesAreEqual(RecipeResponseDto recipe1, RecipeResponseDto recipe2)
    {
        return recipe1.Id == recipe2.Id &&
               recipe1.Title == recipe2.Title &&
               recipe1.Description == recipe2.Description &&
               recipe1.CookingTimeInMinutes == recipe2.CookingTimeInMinutes &&
               _ingredientsComparer.Equals(recipe1.Ingredients, recipe2.Ingredients);
    }
    
    private int GetRecipeHashCode(RecipeResponseDto recipe)
    {
        unchecked
        {
            int hash = 17;
            hash = hash * 23 + recipe.Id.GetHashCode();
            hash = hash * 23 + (recipe.Title?.GetHashCode() ?? 0);
            hash = hash * 23 + (recipe.Description?.GetHashCode() ?? 0);
            hash = hash * 23 + recipe.CookingTimeInMinutes;
            return hash;
        }
    }
}