using System.Collections.Generic;
using HogwartsPotions.Models.Entities;

namespace HogwartsPotions.DTO;

public record struct PotionCreateDto(long StudentId, List<Ingredients> Ingredients);
