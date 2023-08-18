using System.Collections.Generic;
using System.Linq;
using HogwartsPotions.Models;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Models.Enums;

namespace HogwartsPotions.Data
{
    public class HogwartsContextSeed
    {
        public static void ContextSeed(HogwartsContext context)
        {
            if (!context.Students.Any())
            {
                var students = new[]
                {
                    new Student { Name = "Harry Potter", HouseType = HouseType.Gryffindor, PetType = PetType.Owl },
                    new Student { Name = "Hermione Granger", HouseType = HouseType.Gryffindor, PetType = PetType.Cat },
                    new Student { Name = "Ron Weasley", HouseType = HouseType.Gryffindor, PetType = PetType.Rat },
                   
                };

                context.Students.AddRange(students);
                context.SaveChanges();
            }

            if (!context.Rooms.Any())
            {
                var rooms = new[]
                {
                    new Room { Capacity = 25 },
                    new Room { Capacity = 20 },
                    new Room { Capacity = 30 },
                    // Add more rooms here
                };

                context.Rooms.AddRange(rooms);
                context.SaveChanges();
            }

            if (!context.Recipes.Any())
            {
                var recipes = new[]
                {
                    new Recipe { Name = "Polyjuice Potion" },
                    new Recipe { Name = "Felix Felicis" },
                    // Add more recipes here
                };

                context.Recipes.AddRange(recipes);
                context.SaveChanges();
            }

            if (!context.Ingredients.Any())
            {
                var ingredients = new[]
                {
                    new Ingredients { Name = "Lacewing flies" },
                    new Ingredients { Name = "Boomslang skin" },
                    // Add more ingredients here
                };

                context.Ingredients.AddRange(ingredients);
                context.SaveChanges();
            }

            if (!context.Potions.Any())
            {
                var potions = new[]
                {
                    new Potion { Name = "Amortentia", Student = new Student { Name = "Draco Malfoy", HouseType = HouseType.Slytherin, PetType = PetType.None }, Status = BrewingStatus.Brew },
                    new Potion { Name = "Polyjuice Potion", Student = new Student { Name = "Neville Longbottom", HouseType = HouseType.Gryffindor, PetType = PetType.Rat }, Status = BrewingStatus.Brew },
                    // Add more potions here
                };

                foreach (var potion in potions)
                {
                    potion.Ingredients = context.Ingredients.ToList();
                    context.Potions.Add(potion);
                }

                context.SaveChanges();
            }
        }
    }
}
