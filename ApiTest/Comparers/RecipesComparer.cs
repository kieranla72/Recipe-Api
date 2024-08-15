using DB.Models;

namespace ApiTest.Comparers;

public class RecipesComparer : IEqualityComparer<List<Recipe>>
{
    private IngredientsComparer _ingredientsComparer;
    public RecipesComparer()
    {
        _ingredientsComparer = new IngredientsComparer();
    }
    
    public bool Equals(List<Recipe>? list1, List<Recipe>? list2)
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

    public bool Equals(Recipe? recipe1, Recipe? recipe2)
    {
        if (ReferenceEquals(recipe1, recipe2))
            return true;

        if (recipe1 == null || recipe2 == null)
            return false;

        return RecipesAreEqual(recipe1, recipe2);
    }

    public int GetHashCode(List<Recipe> list)
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

    private bool RecipesAreEqual(Recipe recipe1, Recipe recipe2)
    {
        return recipe1.Id == recipe2.Id &&
               recipe1.Title == recipe2.Title &&
               recipe1.Description == recipe2.Description &&
               recipe1.CookingTimeInMinutes == recipe2.CookingTimeInMinutes &&
               _ingredientsComparer.Equals(recipe1.Ingredients, recipe2.Ingredients);
    }
    
    private int GetRecipeHashCode(Recipe recipe)
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