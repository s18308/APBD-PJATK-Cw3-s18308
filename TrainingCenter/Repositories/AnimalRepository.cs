using TrainingCenter.Models;

namespace TrainingCenter.Repositories;

public class AnimalRepository : IAnimalRepository
{
    private static int _nextId = 1;
    private readonly List<Animal> _animals = [];
    
    public IEnumerable<Animal> GetAll()
    {
        return _animals;
    }

    public IEnumerable<Animal> GetBySpecies(string species)
    {
        return _animals.Where(x => x.Species == species);
    }

    public Animal? GetById(int id)
    {
        return _animals.FirstOrDefault(x => x.Id == id);
    }

    public void Add(Animal animal)
    {
        animal.Id = _nextId++;
        _animals.Add(animal);
    }

    public bool Update(Animal animal)
    {
        var existing = GetById(animal.Id);
        if (existing is null)
        {
            return false;
        }
        
        existing.Name = animal.Name;
        existing.Species = animal.Species;
        existing.Age = animal.Age;
        existing.Weight = animal.Weight;
        return true;
    }

    public void Remove(Animal animal)
    {
        _animals.Remove(animal);
    }

    public bool Exists(int id)
    {
        return _animals.Any(x => x.Id == id);
    }
}