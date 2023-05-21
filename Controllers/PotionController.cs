using System.Collections.Generic;
using System.Threading.Tasks;
using HogwartsPotions.DTO;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Services;
using Microsoft.AspNetCore.Mvc;

namespace HogwartsPotions.Controllers;

[Route("/potions")]
[ApiController]
public class PotionController : ControllerBase
{
    private readonly IPotionService _potionService;

    public PotionController(IPotionService potionService)
    {
        _potionService = potionService;
    }

    [HttpGet]
    public async Task<List<Potion>> GetAllPotions()
    {
        return await _potionService.GetAllPotions();
    }

    [HttpGet("{studentId}")]
    public async Task<List<Potion>> GetPotionsOfStudent(long studentId)
    {
        return await _potionService.GetPotionsOfStudent(studentId);
    }

    [HttpGet("{potionId}/help")]
    public async Task<List<Recipe>> GetHelp(long potionId)
    {
        return await _potionService.GetHelpWithRecipes(potionId);
    }

    [HttpPost("brew")]
    public async Task<Potion> AddPotion(PotionCreateDto potion)
    {
        return await _potionService.AddBrewPotion(potion);
    }

    [HttpPut("{potionId}/add")]
    public async Task<Potion> UpdatePotion(long potionId, IngredientCreateDto ingredient)
    {
        return await _potionService.AddIngredient(potionId, ingredient);
    }

    [HttpDelete("{potionId}")]
    public async Task<List<Potion>> DeletePotion(long potionId)
    {
        return await _potionService.DeletePotion(potionId);
    }
}