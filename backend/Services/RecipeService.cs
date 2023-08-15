using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HogwartsPotions.DTO;
using HogwartsPotions.Models;
using HogwartsPotions.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace HogwartsPotions.Services;

public class RecipeService : IRecipeService
{
    private readonly HogwartsContext _context;

    public RecipeService(HogwartsContext context)
    {
        _context = context;
    }

    public async Task<List<Recipe>> GetAllRecipes()
    {
        return await _context.Recipes
            .Include(r => r.Ingredients)
            .ToListAsync();
    }

    public async Task<Recipe> GetRecipe(long id)
    {
        var recipe = await _context.Recipes.FindAsync(id);

        return recipe;
    }


    public async Task<Recipe> AddRecipe(RecipeCreateDto recipe)
    {
        var newRecipe = new Recipe
        {
            Name = recipe.Name
        };
        var student = _context.Students.FirstOrDefault(s => s.Name == recipe.Student.Name);


        newRecipe.Student = student;
        newRecipe.Ingredients = GetIngredients(_context.Ingredients.ToList(), recipe.Ingredients, newRecipe);
        _context.Recipes.Add(newRecipe);
        await _context.SaveChangesAsync();
        return newRecipe;
    }
    
    public List<Ingredients> GetIngredients(List<Ingredients> contextIngredients, List<Ingredients> recipeIngredients,
        Recipe recipe)
    {
        var ingredients = new List<Ingredients>();
        foreach (var ingredient in recipeIngredients)
            if (contextIngredients.Any(i => i.Name == ingredient.Name))
            {
                ingredients.Add(_context.Ingredients.FirstOrDefault(i => i.Name == ingredient.Name));
                ingredient.Recipes = new List<Recipe> { recipe };
            }
            else
            {
                var newIngredient = new Ingredients { Name = ingredient.Name, Recipes = new List<Recipe> { recipe } };
                ingredients.Add(newIngredient);
                contextIngredients.Add(newIngredient);
            }

        return ingredients;
    }

    public async Task DeleteRecipe(long id)
    {
        var recipe = await _context.Recipes.FindAsync(id);
        if (recipe != null) _context.Recipes.Remove(recipe);
        await _context.SaveChangesAsync();
    }
}