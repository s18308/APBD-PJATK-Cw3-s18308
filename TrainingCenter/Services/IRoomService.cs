using TrainingCenter.DTOs;

namespace TrainingCenter.Services;

public interface IRoomService
{
    IEnumerable<RoomDto> GetAll();
    IEnumerable<RoomDto> GetByBuilding(string buildingCode);
    IEnumerable<RoomDto> GetFiltered(int? minCapacity, bool? hasProjector, bool? activeOnly);
    RoomDto GetById(int id);
    RoomDto Add(CreateRoomDto dto);
    RoomDto Update(int id, UpdateRoomDto dto);
    void Remove(int id);
}
