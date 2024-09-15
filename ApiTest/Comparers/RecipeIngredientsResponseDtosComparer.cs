using Api.ResponseModels;
using DB.Models;

namespace ApiTest.Comparers;

public class RecipeIngredientsResponseDtosComparer : IEqualityComparer<List<RecipeIngredientsResponseDto>>
{
    public bool Equals(List<RecipeIngredientsResponseDto>? list1, List<RecipeIngredientsResponseDto>? list2)
    {
        if (ReferenceEquals(list1, list2))
            return true;

        if (list1 == null || list2 == null)
            return false;

        if (list1.Count != list2.Count)
            return false;

        var orderedList1 = list1.OrderByDescending(g => g.Id).ToList();
        var orderedList2 = list2.OrderByDescending(g => g.Id).ToList();

        // Check each Ingredient in both lists
        for (int i = 0; i < list1.Count; i++)
        {
            if (!IngredientResponseDtosAreEqual(orderedList1[i], orderedList2[i]))
                return false;
        }

        return true;
    }

    public bool Equals(RecipeIngredientsResponseDto? ingredient1, RecipeIngredientsResponseDto? ingredient2)
    {
        if (ReferenceEquals(ingredient1, ingredient2))
            return true;

        if (ingredient1 == null || ingredient2 == null)
            return false;

        return IngredientResponseDtosAreEqual(ingredient1, ingredient2);
    }

    public int GetHashCode(List<RecipeIngredientsResponseDto> list)
    {
        unchecked
        {
            int hash = 17;
            foreach (var ingredient in list)
            {
                hash = hash * 23 + GetIngredientHashCode(ingredient);
            }
            return hash;
        }
    }

    private bool IngredientResponseDtosAreEqual(RecipeIngredientsResponseDto ingredient1, RecipeIngredientsResponseDto ingredient2)
    {
        // Compare Id, Title
        return ingredient1.Id == ingredient2.Id &&
               ingredient1.Title == ingredient2.Title &&
               ingredient1.CreatedTimestamp == ingredient2.CreatedTimestamp && 
               ingredient1.Comment == ingredient2.Comment && 
               ingredient1.Quantity == ingredient2.Quantity && 
               ingredient1.UnitOfQuantity == ingredient2.UnitOfQuantity;
    }
    
    private int GetIngredientHashCode(RecipeIngredientsResponseDto ingredient)
    {
        unchecked
        {
            int hash = 17;
            hash = hash * 23 + ingredient.Id.GetHashCode();
            hash = hash * 23 + (ingredient.Title?.GetHashCode() ?? 0);
            hash = hash * 23 + (ingredient.CreatedTimestamp == default ? ingredient.CreatedTimestamp.GetHashCode() : 0);
            hash = hash * 23 + (ingredient.Comment?.GetHashCode() ?? 0);
            hash = hash * 23 + ingredient.Quantity.GetHashCode();
            hash = hash * 23 + ingredient.UnitOfQuantity.GetHashCode();
            return hash;
        }
    }
}