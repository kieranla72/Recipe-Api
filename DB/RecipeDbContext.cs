using DB.Models;
using Microsoft.EntityFrameworkCore;

namespace DB;

public class RecipeDbContext: DbContext
{
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<RecipeIngredient> RecipeIngredients { get; set; }

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