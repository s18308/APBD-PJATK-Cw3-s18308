using TrainingCenter.DTOs;
using TrainingCenter.Exceptions;
using TrainingCenter.Mappers;
using TrainingCenter.Repositories;

namespace TrainingCenter.Services;

public class ReservationService(IReservationRepository reservationRepository, IRoomRepository roomRepository) : IReservationService
{
    public IEnumerable<ReservationDto> GetAll() =>
        reservationRepository.GetAll().Select(r => r.ToDto());

    public IEnumerable<ReservationDto> GetFiltered(DateOnly? date, string? status, int? roomId) =>
        reservationRepository.GetFiltered(date, status, roomId).Select(r => r.ToDto());

    public ReservationDto GetById(int id)
    {
        var reservation = reservationRepository.GetById(id);
        return reservation is null ? throw new ReservationNotFoundException(id) : reservation.ToDto();
    }

    public ReservationDto Add(CreateReservationDto dto)
    {
        var room = roomRepository.GetById(dto.RoomId);
        if (room is null)
            throw new RoomNotFoundException(dto.RoomId);

        if (!room.IsActive)
            throw new RoomInactiveException(dto.RoomId);

        if (reservationRepository.HasConflict(dto.RoomId, dto.Date, dto.StartTime, dto.EndTime))
            throw new ReservationConflictException(dto.RoomId, dto.Date);

        var reservation = dto.ToDomain();
        reservationRepository.Add(reservation);
        return reservation.ToDto();
    }

    public ReservationDto Update(int id, UpdateReservationDto dto)
    {
        var existing = reservationRepository.GetById(id);
        if (existing is null)
            throw new ReservationNotFoundException(id);

        var room = roomRepository.GetById(dto.RoomId);
        if (room is null)
            throw new RoomNotFoundException(dto.RoomId);

        if (!room.IsActive)
            throw new RoomInactiveException(dto.RoomId);

        if (reservationRepository.HasConflict(dto.RoomId, dto.Date, dto.StartTime, dto.EndTime, excludeId: id))
            throw new ReservationConflictException(dto.RoomId, dto.Date);

        var reservation = dto.ToDomain();
        reservation.Id = id;
        reservationRepository.Update(reservation);
        return reservation.ToDto();
    }

    public void Remove(int id)
    {
        var reservation = reservationRepository.GetById(id);
        if (reservation is null)
            throw new ReservationNotFoundException(id);

        reservationRepository.Remove(reservation);
    }
}
