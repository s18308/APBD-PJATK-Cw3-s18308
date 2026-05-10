using TrainingCenter.DTOs;
using TrainingCenter.Exceptions;
using TrainingCenter.Mappers;
using TrainingCenter.Repositories;

namespace TrainingCenter.Services;

public class RoomService(IRoomRepository roomRepository, IReservationRepository reservationRepository) : IRoomService
{
    public IEnumerable<RoomDto> GetAll() =>
        roomRepository.GetAll().Select(r => r.ToDto());

    public IEnumerable<RoomDto> GetByBuilding(string buildingCode) =>
        roomRepository.GetByBuilding(buildingCode).Select(r => r.ToDto());

    public IEnumerable<RoomDto> GetFiltered(int? minCapacity, bool? hasProjector, bool? activeOnly) =>
        roomRepository.GetFiltered(minCapacity, hasProjector, activeOnly).Select(r => r.ToDto());

    public RoomDto GetById(int id)
    {
        var room = roomRepository.GetById(id);
        return room is null ? throw new RoomNotFoundException(id) : room.ToDto();
    }

    public RoomDto Add(CreateRoomDto dto)
    {
        var room = dto.ToDomain();
        roomRepository.Add(room);
        return room.ToDto();
    }

    public RoomDto Update(int id, UpdateRoomDto dto)
    {
        var room = dto.ToDomain();
        room.Id = id;
        return !roomRepository.Update(room)
            ? throw new RoomNotFoundException(id)
            : room.ToDto();
    }

    public void Remove(int id)
    {
        var room = roomRepository.GetById(id);
        if (room is null)
            throw new RoomNotFoundException(id);

        var futureReservations = reservationRepository.GetFutureByRoom(id);
        if (futureReservations.Any())
            throw new RoomHasFutureReservationsException(id);

        roomRepository.Remove(room);
    }
}
