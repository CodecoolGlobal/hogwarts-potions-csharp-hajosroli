using System.Collections.Generic;
using System.Threading.Tasks;
using HogwartsPotions.DTO;
using HogwartsPotions.Models.Entities;

namespace HogwartsPotions.Services;

public interface IRecipeService
{
    Task<List<Recipe>> GetAllRecipes();
    Task<Recipe> GetRecipe(long id);
    Task<Recipe> AddRecipe(RecipeCreateDto recipe);

    List<Ingredients> GetIngredients(List<Ingredients> contextIngredients, List<Ingredients> ingredients,
        Recipe recipe);

    Task DeleteRecipe(long id);
}