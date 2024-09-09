using DB.Models;

namespace LibTest;

public class TestsBase
{
    public TestsBase() {}
    
    protected List<Ingredient> GetIngredients()
    {
        return
        [
            new() { Title = "Potatoes", Id = 1 }, // Roast dinner
            new() { Title = "Chicken", Id = 2 }, // Roast dinner
            new() { Title = "Cheese", Id = 3 }, // Toastie
        ];
    }
    protected List<Recipe> GetRecipes()
    {
        return
        [
            new() { Title = "Roast Dinner", CookingTimeInMinutes = 120, Description = "A Beautifully cooked roast dinner"},
            new() { Title = "Toastie", CookingTimeInMinutes = 20, Description = "A cheesy hot mess"},
        ];
    }
}