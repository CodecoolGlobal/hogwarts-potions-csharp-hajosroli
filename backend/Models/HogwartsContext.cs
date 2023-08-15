using HogwartsPotions.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace HogwartsPotions.Models;

public class HogwartsContext : DbContext
{
    public HogwartsContext(DbContextOptions<HogwartsContext> options) : base(options)
    {
    }

    public DbSet<Student> Students { get; set; }
    public DbSet<Room> Rooms { get; set; }

    public DbSet<Recipe> Recipes { get; set; }

    public DbSet<Ingredients> Ingredients { get; set; }

    public DbSet<Potion> Potions { get; set; }
}