using TrainingCenter.DTOs;

namespace TrainingCenter.Services;

public interface IAnimalService
{
    IEnumerable<AnimalDto> GetAll(string? species);
    AnimalDto GetById(int id);
    AnimalDto Add(CreateAnimalDto animal);
    AnimalDto Update(int id, UpdateAnimalDto animal);
    void Remove(int id);
}