using System.Collections.Generic;
using System.Threading.Tasks;
using HogwartsPotions.DTO;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Models.Enums;

namespace HogwartsPotions.Services;

public interface IPotionService
{
    Task<Potion> AddBrewPotion(PotionCreateDto brewPotion);

    //Task<Potion> AddPotion(PotionCreateDto potion);
    Task<List<Potion>> GetPotionsOfStudent(long studentId);
    Task<Potion> GetPotion(long potionId);
    Task<List<Potion>> GetBrewPotions();
    Task<List<Potion>> GetAllPotions();
    Task<Potion> AddIngredient(long potionId, IngredientCreateDto ingredient);
    Task<List<Recipe>> GetHelpWithRecipes(long potionId);
    Task<List<Potion>> DeletePotion(long id);
    Task<BrewingStatus> GetStatusOfPotion(long id);
}