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
        //var student = await _context.Students.FirstOrDefault(s => s.Name == potion.Student.Name);
        var result = await Task.Run(() =>
        {
            var student = _context.Students.FirstOrDefault(s => s.Name == potion.Student.Name);
            Console.WriteLine(student);
            var recipes = _context.Recipes
                .Include(r => r.Ingredients)
                .ToList();
            
            newPotion.Status = SetBrewingStatus(potion, recipes);
            Console.WriteLine("here");
            Console.WriteLine(newPotion.Status);
            if ( newPotion.Status == BrewingStatus.Discovery)
            {
                _context.Recipes.Add(AddNewRecipe(potion));
            }

            newPotion.Ingredients = potion.Ingredients.Select(i => i).ToList();
            newPotion.Student = student;
            newPotion.Recipe = AddNewRecipe(potion);
            _context.Potions.Add(newPotion); 
            _context.SaveChanges();
            return newPotion;
            
        });
        return result;

    }
    
    private BrewingStatus SetBrewingStatus(PotionCreateDto potion, List<Recipe> recipes )
    {
        Console.WriteLine("bbbbb");
        Console.WriteLine($"itt van {CheckIngredients(potion, recipes)}");
        Console.WriteLine(potion.Ingredients.Count);
        var status = BrewingStatus.Brew;
        if (potion.Ingredients.Count < 5)
        {
            Console.WriteLine("iiiiii");
            status = BrewingStatus.Brew;
        }
        else if (potion.Ingredients.Count >= 5 && CheckIngredients(potion, recipes))
        {
            
            status = BrewingStatus.Replica;
        }
        else
        {
            Console.WriteLine("aaaaa");
            status = BrewingStatus.Discovery;
        }

        return status;
    }

    private Recipe AddNewRecipe(PotionCreateDto potion)
    {
        var newRecipe = new Recipe
        {
            Ingredients = potion.Ingredients
        };
        var student = _context.Students.FirstOrDefault(s => s.Name == potion.Student.Name);
        newRecipe.Student = student;
        newRecipe.Name = $"{potion.Student.Name}'s discovery#{newRecipe.Id}";

        return newRecipe;
    }
    private bool CheckIngredients(PotionCreateDto potion, List<Recipe> recipes)
    {
        bool result = false;
        Console.WriteLine("ez" + recipes.First().Ingredients.Count);
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
            result = counter == potion.Ingredients.Count;
        }
        return result;
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