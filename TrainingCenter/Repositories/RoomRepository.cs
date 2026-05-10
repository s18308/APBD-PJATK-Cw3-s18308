using TrainingCenter.Models;

namespace TrainingCenter.Repositories;

public class RoomRepository : IRoomRepository
{
    private static int _nextId = 6;
    private readonly List<Room> _rooms =
    [
        new() { Id = 1, Name = "Sala A1", BuildingCode = "A", Floor = 0, Capacity = 30, HasProjector = true,  IsActive = true  },
        new() { Id = 2, Name = "Lab 204", BuildingCode = "B", Floor = 2, Capacity = 24, HasProjector = true,  IsActive = true  },
        new() { Id = 3, Name = "Sala B3", BuildingCode = "B", Floor = 3, Capacity = 15, HasProjector = false, IsActive = true  },
        new() { Id = 4, Name = "Aula C",  BuildingCode = "C", Floor = 1, Capacity = 80, HasProjector = true,  IsActive = true  },
        new() { Id = 5, Name = "Sala D1", BuildingCode = "D", Floor = 0, Capacity = 20, HasProjector = false, IsActive = false },
    ];

    public IEnumerable<Room> GetAll() => _rooms;

    public IEnumerable<Room> GetByBuilding(string buildingCode) =>
        _rooms.Where(r => r.BuildingCode.Equals(buildingCode, StringComparison.OrdinalIgnoreCase));

    public IEnumerable<Room> GetFiltered(int? minCapacity, bool? hasProjector, bool? activeOnly)
    {
        var query = _rooms.AsEnumerable();
        if (minCapacity.HasValue)
            query = query.Where(r => r.Capacity >= minCapacity.Value);
        if (hasProjector.HasValue)
            query = query.Where(r => r.HasProjector == hasProjector.Value);
        if (activeOnly == true)
            query = query.Where(r => r.IsActive);
        return query;
    }

    public Room? GetById(int id) => _rooms.FirstOrDefault(r => r.Id == id);

    public void Add(Room room)
    {
        room.Id = _nextId++;
        _rooms.Add(room);
    }

    public bool Update(Room room)
    {
        var existing = GetById(room.Id);
        if (existing is null) return false;

        existing.Name = room.Name;
        existing.BuildingCode = room.BuildingCode;
        existing.Floor = room.Floor;
        existing.Capacity = room.Capacity;
        existing.HasProjector = room.HasProjector;
        existing.IsActive = room.IsActive;
        return true;
    }

    public void Remove(Room room) => _rooms.Remove(room);
}
