using TrainingCenter.DTOs;
using TrainingCenter.Models;

namespace TrainingCenter.Mappers;

public static class AnimalMappingExtensions
{
    public static Animal ToDomain(this AnimalDto dto)
    {
        return new Animal
        {
            Id = dto.Id,
            Name = dto.Name,
            Age = dto.Age,
            Weight = dto.Weight,
            Species = dto.Species,
        };
    }

    public static Animal ToDomain(this CreateAnimalDto dto)
    {
        return new Animal
        {
            Name = dto.Name,
            Age = dto.Age,
            Weight = dto.Weight,
            Species = dto.Species,
        };
    }

    public static Animal ToDomain(this UpdateAnimalDto dto)
    {
        return new Animal
        {
            Name = dto.Name,
            Age = dto.Age,
            Weight = dto.Weight,
            Species = dto.Species,
        };
    }
    
    public static AnimalDto ToDto(this Animal animal)
    {
        return new AnimalDto
        {
            Id = animal.Id,
            Name = animal.Name,
            Age = animal.Age,
            Weight = animal.Weight,
            Species = animal.Species,
        };
    }
}