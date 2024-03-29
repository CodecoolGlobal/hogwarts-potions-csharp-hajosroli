using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HogwartsPotions.DTO;
using HogwartsPotions.Models;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace HogwartsPotions.Services;

public class PotionService : IPotionService
{
    private readonly HogwartsContext _context;
    private readonly IRecipeService _recipeService;

    public PotionService(HogwartsContext context, IRecipeService recipeService)
    {
        _context = context;
        _recipeService = recipeService;
    }


    public async Task<Potion> AddBrewPotion(PotionCreateDto brewPotion)
    {
        var newBrewPotion = new Potion();

       
      
            var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == brewPotion.StudentId);
            newBrewPotion.Student = student;
            var recipes = await _context.Recipes
                .Include(r => r.Ingredients)
                .ToListAsync();
            newBrewPotion.Status = BrewingStatus.Brew;
            _context.Potions.Add(newBrewPotion);
            await _context.SaveChangesAsync();
            return newBrewPotion;
      

        
    }
    
    
    
    public async Task<List<Potion>> GetPotionsOfStudent(long studentId)
    {
        return await _context.Potions
            .Include(p => p.Student)
            .Where(potion => potion.Student.Id == studentId)
            .ToListAsync();
    }

    public async Task<Potion> GetPotion(long potionId)
    {
        return await _context.Potions
                .Include(p => p.Student)
                .Include(p => p.Ingredients)
                .Include(p => p.Recipe)
                .FirstOrDefaultAsync(p => p.Id == potionId);
    }

    public async Task<List<Potion>> GetBrewPotions()
    {
        return await _context.Potions
            .Where(p => p.Status == BrewingStatus.Brew)
            .Include(p => p.Student)
            .Include(p => p.Ingredients)
            .Include(p => p.Recipe)
            .ToListAsync();
    }

    public async Task<List<Potion>> GetAllPotions()
    {
        return await _context.Potions
            .Include(p => p.Student)
            .Include(p => p.Recipe)
            .Include(p => p.Ingredients)
            .ToListAsync();
    }

    public async Task<Potion> AddIngredient(long potionId, IngredientCreateDto ingredient)
    {
        var potion = await _context.Potions
            .Include(p => p.Ingredients)
            .Include(p => p.Student)
            .Include(p => p.Recipe)
            .FirstOrDefaultAsync(p => p.Id == potionId);
        var recipes = _context.Recipes
            .Include(r => r.Ingredients)
            .ToList();
        var ingredients = await _context.Ingredients.ToListAsync();
        
        var newIngredient = CheckIngredientIfExist(ingredient, ingredients);
        potion.Ingredients.Add(newIngredient);
        potion.Status = SetBrewingStatus(potion, recipes);

        if (potion.Recipe != null)
            potion.Recipe.Ingredients.Add(newIngredient);
        else if (potion.Status == BrewingStatus.Discovery && potion.Recipe == null)
            potion.Recipe = AddNewRecipe(potion);
        else if (potion.Status == BrewingStatus.Replica && potion.Recipe == null)
            potion.Recipe = ReplicaRecipe(potion, recipes);
        await _context.SaveChangesAsync();
        return potion;
    }

    public async Task<List<Recipe>> GetHelpWithRecipes(long potionId)
    {
        var potion = await _context.Potions
            .Include(p => p.Ingredients)
            .Include(p => p.Recipe)
            .FirstOrDefaultAsync(p => p.Id == potionId);
        var recipes = _context.Recipes
            .Include(r => r.Ingredients)
            .ToList();
        return recipes.Where(recipe => CheckIngredientsOfRecipe(potion, recipe)).ToList();
    }

    public async Task<List<Potion>> DeletePotion(long id)
    {
        var potion = await _context.Potions.FindAsync(id);
        if (potion != null) _context.Potions.Remove(potion);


        await _context.SaveChangesAsync();
        return await _context.Potions.ToListAsync();

    }

    public async Task<BrewingStatus> GetStatusOfPotion(long id)
    {
        var potion = await _context.Potions.FindAsync(id);
        if (potion != null) return potion.Status;
        throw new InvalidOperationException();
    }

    private BrewingStatus SetBrewingStatus(Potion potion, List<Recipe> recipes)
    {
        var status = BrewingStatus.Brew;
        if (potion.Ingredients.Count < 5)
            status = BrewingStatus.Brew;
        else if (potion.Ingredients.Count >= 5 && CheckAllRecipesIngredients(potion, recipes))
            status = BrewingStatus.Replica;
        else
            status = BrewingStatus.Discovery;

        return status;
    }

    private Recipe AddNewRecipe(Potion potion)
    {
        var newRecipe = new Recipe();
        var student = _context.Students.FirstOrDefault(s => s.Id == potion.Student.Id);
        newRecipe.Student = student;
        if (student != null)
            newRecipe.Name = $"{student.Name}'s discovery#{_context.Recipes.Count(r => r.Student == student) + 1}";
        newRecipe.Ingredients =
            _recipeService.GetIngredients(_context.Ingredients.ToList(), potion.Ingredients, newRecipe);
        return newRecipe;
    }


    private bool CheckAllRecipesIngredients(Potion potion, List<Recipe> recipes)
    {
        var result = false;
        foreach (var contextRecipe in recipes)
        {
            result = CheckIngredientsOfRecipe(potion, contextRecipe);
        }
        return result;
    }

    private bool CheckIngredientsOfRecipe(Potion potion, Recipe recipe)
    {
        var counter = 0;
        var ingredients = recipe.Ingredients;
        foreach (var contextRecipeIngredient in ingredients)
        foreach (var potionIngredient in potion.Ingredients)
        {
            if (contextRecipeIngredient.Name == potionIngredient.Name) counter++;
        }
        return counter == potion.Ingredients.Count;
    }
    private Recipe ReplicaRecipe(Potion potion, List<Recipe> recipes)
    {
        return recipes.FirstOrDefault(contextRecipe => CheckIngredientsOfRecipe(potion, contextRecipe));
    }
    
    private Ingredients CheckIngredientIfExist(IngredientCreateDto ingredient, List<Ingredients> contextIngredients)
    {
        if (contextIngredients.All(i => i.Name != ingredient.Name))
        {
            var newIngredient = new Ingredients { Name = ingredient.Name, Recipes = new List<Recipe>() };
            contextIngredients.Add(newIngredient);
            return newIngredient;
        }

        return contextIngredients.FirstOrDefault(i => i.Name == ingredient.Name);
    }
    
}