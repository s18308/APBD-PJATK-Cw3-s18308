using TrainingCenter.Models;

namespace TrainingCenter.Repositories;

public interface IRoomRepository
{
    IEnumerable<Room> GetAll();
    IEnumerable<Room> GetByBuilding(string buildingCode);
    IEnumerable<Room> GetFiltered(int? minCapacity, bool? hasProjector, bool? activeOnly);
    Room? GetById(int id);
    void Add(Room room);
    bool Update(Room room);
    void Remove(Room room);
}
