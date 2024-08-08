using DB.Models;

namespace ApiTest.Comparers;

public class IngredientsComparer : IEqualityComparer<List<Ingredient>>
{
    public bool Equals(List<Ingredient>? list1, List<Ingredient>? list2)
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
            if (!IngredientsAreEqual(orderedList1[i], orderedList2[i]))
                return false;
        }

        return true;
    }

    public bool Equals(Ingredient? ingredient1, Ingredient? ingredient2)
    {
        if (ReferenceEquals(ingredient1, ingredient2))
            return true;

        if (ingredient1 == null || ingredient2 == null)
            return false;

        return IngredientsAreEqual(ingredient1, ingredient2);
    }

    public int GetHashCode(List<Ingredient> list)
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

    private bool IngredientsAreEqual(Ingredient ingredient1, Ingredient ingredient2)
    {
        // Compare Id, Title
        return ingredient1.Id == ingredient2.Id &&
               ingredient1.Title == ingredient2.Title;
    }
    
    private int GetIngredientHashCode(Ingredient ingredient)
    {
        unchecked
        {
            int hash = 17;
            hash = hash * 23 + ingredient.Id.GetHashCode();
            hash = hash * 23 + (ingredient.Title?.GetHashCode() ?? 0);
            return hash;
        }
    }
}