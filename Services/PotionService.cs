using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HogwartsPotions.DTO;
using HogwartsPotions.Models;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HogwartsPotions.Services;

public class PotionService: IPotionService
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

        var result = await Task.Run(() =>
        {
            var student = _context.Students.FirstOrDefault(s => s.Id == brewPotion.StudentId);
            newBrewPotion.Student = student;
            var recipes = _context.Recipes
                .Include(r => r.Ingredients)
                .ToList();
            newBrewPotion.Status = SetBrewingStatus(brewPotion, recipes);
            _context.Potions.Add(newBrewPotion);
            _context.SaveChanges();
            return newBrewPotion;
        });

        return result;

    }
    

    /*public async Task<Potion> AddPotion(PotionCreateDto potion)
    {
        var newPotion = new Potion
        {
            Name = potion.Name,
        };
        var result = await Task.Run(() =>
        {
            var student = _context.Students.FirstOrDefault(s => s.Name == potion.Student.Name);
            Console.WriteLine(student);
            var recipes = _context.Recipes
                .Include(r => r.Ingredients)
                .ToList();
            
            newPotion.Status = SetBrewingStatus(potion, recipes);
            Console.WriteLine(newPotion.Status);
            newPotion.Recipe = newPotion.Status == BrewingStatus.Discovery ? AddNewRecipe(potion) : CheckIngredients(potion, recipes);
            newPotion.Ingredients = newPotion.Recipe.Ingredients;//_recipeService.GetIngredients(_context.Ingredients.ToList(), potion.Ingredients, newPotion.Recipe);
            newPotion.Student = student;
            _context.Potions.Add(newPotion); 
            _context.SaveChanges();
            return newPotion;
        });
        return result;
    }*/

   
    public async Task<List<Potion>> GetPotionsOfStudent(long studentId)
    {
        return await _context.Potions
            .Include(p => p.Student)
            .Where(potion => potion.Student.Id == studentId)
            .ToListAsync();
    }

    private BrewingStatus SetBrewingStatus(PotionCreateDto potion, List<Recipe> recipes )
    {
        var status = BrewingStatus.Brew;
        if (potion.Ingredients.Count < 5)
        {
            status = BrewingStatus.Brew;
        }
        else if (potion.Ingredients.Count >= 5 && CheckIngredients(potion, recipes) != null)
        {
            status = BrewingStatus.Replica;
        }
        else
        {
            status = BrewingStatus.Discovery;
        }
        return status;
    }

    private Recipe AddNewRecipe(PotionCreateDto potion)
    {
        var newRecipe = new Recipe();
        var student = _context.Students.FirstOrDefault(s => s.Id == potion.StudentId);
        newRecipe.Student = student;
        if (student != null)
            newRecipe.Name = $"{student.Name}'s discovery#{_context.Recipes.Count(r => r.Student == student) + 1}";
        newRecipe.Ingredients = _recipeService.GetIngredients(_context.Ingredients.ToList(), potion.Ingredients, newRecipe);
        return newRecipe;
    }
    
    
    private Recipe CheckIngredients(PotionCreateDto potion, List<Recipe> recipes)
    {
        foreach (var contextRecipe in recipes)
        {  
            int counter = 0;
            var ingredients = contextRecipe.Ingredients;
            foreach (var contextRecipeIngredient in  ingredients)
            {
                foreach (var potionIngredient in potion.Ingredients)
                {
                    Console.WriteLine($"potion: {potionIngredient.Id}{potionIngredient.Name}");
                    if (contextRecipeIngredient.Name == potionIngredient.Name)
                    {
                        counter++;
                    }
                }
            }
            if (counter == potion.Ingredients.Count)
            {
                return contextRecipe;
            }
        }

        return null;
    }
    

    public Task<Potion> GetPotion(long potionId)
    {
        throw new System.NotImplementedException();
    }

    public async Task<List<Potion>> GetAllPotions()
    {
        return await _context.Potions
            .Include(p => p.Student)
            .ToListAsync();
    }

    public async Task<Potion> AddIngredient(long potionId, IngredientCreateDto ingredient)
    {
        var potion = await _context.Potions
            .Include(p => p.Ingredients)
            .Include(p => p.Student)
            .FirstOrDefaultAsync(p => p.Id == potionId);
        
            Console.WriteLine(potion.Id);
            Console.WriteLine(potion.Student.Name);
            //potion.Recipe = potion.Status == BrewingStatus.Discovery ? AddNewRecipe(potion) : CheckIngredients(potion, recipes);
            var ingredients = await _context.Ingredients.ToListAsync();
            Console.WriteLine(ingredients.Count);
            Console.WriteLine(potion.Recipe.Id);
            Console.WriteLine(potion.Recipe.Name);
            Console.WriteLine(CheckIngredientIfExist(ingredient, ingredients, potion.Recipe));
            potion.Ingredients.Add(CheckIngredientIfExist(ingredient, ingredients, potion.Recipe));
            Console.WriteLine(potion.Ingredients.First().Name);
            Console.WriteLine(potion.Ingredients.First().Recipes.First().Name);
            potion.Name = $"{potion.Student.Name}'s {potion.Ingredients.First().Name} Potion";
            Console.WriteLine(potion.Name);
            await _context.SaveChangesAsync();
            Console.WriteLine(potion.Ingredients.Count);
            return potion;
        
            
    }

    private Ingredients CheckIngredientIfExist(IngredientCreateDto ingredient, List<Ingredients> contextIngredients, Recipe recipe)
    {
       
            if (contextIngredients.All(i => i.Name != ingredient.Name))
            {
                var newIngredient = new Ingredients{Name = ingredient.Name, Recipes = new List<Recipe>{recipe}};
                contextIngredients.Add(newIngredient);
                return newIngredient;
            }
            else
            {
                return contextIngredients.FirstOrDefault(i => i.Name == ingredient.Name);
            }
    }

    public Task UpdatePotion(Potion potion)
    {
        throw new System.NotImplementedException();
    }

    public Task DeletePotion(long id)
    {
        throw new System.NotImplementedException();
    }
}