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

public class PotionService: IPotionService
{
    private readonly HogwartsContext _context;
    private readonly IRecipeService _recipeService;

    public PotionService(HogwartsContext context, IRecipeService recipeService)
    {
        _context = context;
        _recipeService = recipeService;
    }

    public async Task<Potion> AddPotion(PotionCreateDto potion)
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
        var newRecipe = new Recipe
        {
            
        };
        var student = _context.Students.FirstOrDefault(s => s.Name == potion.Student.Name);
        newRecipe.Student = student;
        newRecipe.Name = $"{potion.Student.Name}'s discovery#{_context.Recipes.Count(r => r.Student == student) + 1}";
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

    public Task<List<Potion>> GetAllPotions()
    {
        throw new System.NotImplementedException();
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