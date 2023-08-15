using System.Collections.Generic;
using HogwartsPotions.Models.Entities;

namespace HogwartsPotions.DTO;

public record struct RecipeCreateDto(string Name, StudentCreateDto Student, List<Ingredients> Ingredients);