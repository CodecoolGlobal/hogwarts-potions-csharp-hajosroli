using System.Collections.Generic;
using HogwartsPotions.Models.Entities;

namespace HogwartsPotions.DTO;

public record struct IngredientCreateDto(string Name, List<Recipe> Recipes);
