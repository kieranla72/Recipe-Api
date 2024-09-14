using DB.Models;
using Microsoft.EntityFrameworkCore;

namespace DB;

public class RecipeDbContext: DbContext
{
    public DbSet<Ingredient> Ingredients { get; init; }
    public DbSet<Recipe> Recipes { get; init; }
    public DbSet<RecipeIngredient> RecipeIngredients { get; init; }
    public DbSet<RecipeGroup> RecipeGroups { get; init; }
    public DbSet<RecipeGroupRecipe> RecipeGroupRecipes { get; init; }

    public RecipeDbContext(DbContextOptions<RecipeDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Recipe>()
            .HasMany<Ingredient>(e => e.Ingredients)
            .WithMany(e => e.Recipes)
            .UsingEntity<RecipeIngredient>();
    }

}