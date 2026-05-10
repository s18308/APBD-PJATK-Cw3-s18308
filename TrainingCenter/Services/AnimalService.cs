using TrainingCenter.DTOs;
using TrainingCenter.Exceptions;
using TrainingCenter.Mappers;
using TrainingCenter.Repositories;

namespace TrainingCenter.Services;

public class AnimalService(IAnimalRepository repository) : IAnimalService
{
    public IEnumerable<AnimalDto> GetAll(string? species)
    {
        return (string.IsNullOrEmpty(species)
            ? repository.GetAll()
            : repository.GetBySpecies(species))
            .Select(animal => animal.ToDto());
    }

    public AnimalDto GetById(int id)
    {
        var animal = repository.GetById(id);

        return animal is null 
            ? throw new AnimalNotFoundException(id) 
            : animal.ToDto();
    }

    public AnimalDto Add(CreateAnimalDto animal)
    {
        var animalToAdd = animal.ToDomain();
        repository.Add(animalToAdd);
        
        return animalToAdd.ToDto();
    }

    public AnimalDto Update(int id, UpdateAnimalDto animal)
    {
        var animalToUpdate = animal.ToDomain();
        animalToUpdate.Id = id;

        return !repository.Update(animalToUpdate) 
            ? throw new AnimalNotFoundException(id) 
            : animalToUpdate.ToDto();
    }

    public void Remove(int id)
    {
        var animalToDelete = repository.GetById(id);
        
        if (animalToDelete is null)
        {
            throw new AnimalNotFoundException(id);
        }
        
        repository.Remove(animalToDelete);
    }
}