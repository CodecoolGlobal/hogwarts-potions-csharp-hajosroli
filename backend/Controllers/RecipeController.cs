using System.Collections.Generic;
using System.Threading.Tasks;
using HogwartsPotions.DTO;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Services;
using Microsoft.AspNetCore.Mvc;

namespace HogwartsPotions.Controllers;

[Route("api/recipes")]
[ApiController]
public class RecipeController : ControllerBase
{
    private readonly IRecipeService _recipeService;

    public RecipeController(IRecipeService recipeService)
    {
        _recipeService = recipeService;
    }

    // GET: api/recipes
    [HttpGet]
    public async Task<List<Recipe>> GetRecipes()
    {
        return await _recipeService.GetAllRecipes();
    }

    // GET: api/recipes/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Recipe>> GetRecipe(long id)
    {
        var recipe = await _recipeService.GetRecipe(id);

        if (recipe == null) return NotFound();

        return recipe;
    }

    /*[HttpGet("ingredients/{id}")]
    public async Task<IEnumerable<Ingredients>> GetIngredientsOfARecipe(long id)
    {
        return await _recipeService.GetIngredients(id);
    }*/

    
    // POST: api/Reci
    [HttpPost]
    public async Task<List<Recipe>> PostRecipe(RecipeCreateDto recipe)
    {
        await _recipeService.AddRecipe(recipe);


        return await _recipeService.GetAllRecipes();
    }

    // DELETE: api/Recipe/5
    [HttpDelete("{id}")]
    public async Task<List<Recipe>> DeleteRecipe(long id)
    {
        await _recipeService.DeleteRecipe(id);

        return await _recipeService.GetAllRecipes();
    }
}