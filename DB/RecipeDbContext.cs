using DB.Models;
using Microsoft.EntityFrameworkCore;

namespace DB;

public class RecipeDbContext: DbContext
{
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<Recipe> Recipes { get; set; }

    public RecipeDbContext(DbContextOptions<RecipeDbContext> options)
        : base(options)
    {
        var x = "hello";
    }

}