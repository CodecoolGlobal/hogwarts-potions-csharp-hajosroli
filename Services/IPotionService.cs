using System.Collections.Generic;
using System.Threading.Tasks;
using HogwartsPotions.DTO;
using HogwartsPotions.Models.Entities;

namespace HogwartsPotions.Services;

public interface IPotionService
{
    Task<Potion> AddPotion(PotionCreateDto potion);
    Task<Potion> GetPotion(long potionId);
    Task<List<Potion>> GetAllPotions();
    Task UpdatePotion(Potion potion);
    Task DeletePotion(long id);
}