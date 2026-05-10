using TrainingCenter.DTOs;
using TrainingCenter.Models;

namespace TrainingCenter.Mappers;

public static class ReservationMappingExtensions
{
    public static Reservation ToDomain(this CreateReservationDto dto) => new()
    {
        RoomId = dto.RoomId,
        OrganizerName = dto.OrganizerName,
        Topic = dto.Topic,
        Date = dto.Date,
        StartTime = dto.StartTime,
        EndTime = dto.EndTime,
        Status = dto.Status,
    };

    public static Reservation ToDomain(this UpdateReservationDto dto) => new()
    {
        RoomId = dto.RoomId,
        OrganizerName = dto.OrganizerName,
        Topic = dto.Topic,
        Date = dto.Date,
        StartTime = dto.StartTime,
        EndTime = dto.EndTime,
        Status = dto.Status,
    };

    public static ReservationDto ToDto(this Reservation reservation) => new()
    {
        Id = reservation.Id,
        RoomId = reservation.RoomId,
        OrganizerName = reservation.OrganizerName,
        Topic = reservation.Topic,
        Date = reservation.Date,
        StartTime = reservation.StartTime,
        EndTime = reservation.EndTime,
        Status = reservation.Status,
    };
}
