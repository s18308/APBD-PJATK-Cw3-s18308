using TrainingCenter.Models;

namespace TrainingCenter.Repositories;

public interface IAnimalRepository
{
    IEnumerable<Animal> GetAll(); 
    IEnumerable<Animal> GetBySpecies(string species);
    Animal? GetById(int id);
    void Add(Animal animal);
    bool Update(Animal animal);
    void Remove(Animal animal);
    bool Exists(int id);
}