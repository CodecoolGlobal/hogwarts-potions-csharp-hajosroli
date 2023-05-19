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
        return await _context.Recipes.ToListAsync();
    }

    public async Task<Recipe> GetRecipe(long id)
    {
        var recipe = await _context.Recipes.FindAsync(id);

        return recipe;
    }
    

    public async Task<Recipe> AddRecipe(RecipeCreateDto recipe)
    {
        var newRecipe = new Recipe()
        {
            Name = recipe.Name
        };
        var student = _context.Students.FirstOrDefault(s => s.Name == recipe.Student.Name);
        /*var ingredients = recipe.Ingredients
            .Select(i => new Ingredients { Name = i.Name, Recipes = new List<Recipe> { newRecipe } }).ToList();*/
        var ingredients2 = new List<Ingredients>();
        
        foreach (var ingredient in recipe.Ingredients)
        {
            if (_context.Ingredients.Any(i => i.Name == ingredient.Name))
            {
                ingredients2.Add(_context.Ingredients.FirstOrDefault(i => i.Name == ingredient.Name));
                ingredient.Recipes = new List<Recipe> { newRecipe };
            }
            else
            {
                var newIngredient = new Ingredients{Name = ingredient.Name, Recipes = new List<Recipe>{newRecipe}};
                ingredients2.Add(newIngredient);
                _context.Ingredients.Add(newIngredient);
            } 
        }
        newRecipe.Student = student;
        newRecipe.Ingredients = ingredients2;
        _context.Recipes.Add(newRecipe);
        await _context.SaveChangesAsync();
        return newRecipe;
    }
    
    //public void AddIngredientsToRecipe()

    public IEnumerable<Ingredients> GetIngredients(long id)
    {
        var ingredients =  _context.Recipes
            .Where(r => r.Id == id)
            .SelectMany(r => r.Ingredients)
            .ToList();
        return ingredients;
    }
    public async Task DeleteRecipe(long id)
    {
        var recipe = await _context.Recipes.FindAsync(id);
        if (recipe != null) _context.Recipes.Remove(recipe);
        await _context.SaveChangesAsync();
    }
}