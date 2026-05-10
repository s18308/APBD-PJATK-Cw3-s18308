using TrainingCenter.DTOs;

namespace TrainingCenter.Services;

public interface IReservationService
{
    IEnumerable<ReservationDto> GetAll();
    IEnumerable<ReservationDto> GetFiltered(DateOnly? date, string? status, int? roomId);
    ReservationDto GetById(int id);
    ReservationDto Add(CreateReservationDto dto);
    ReservationDto Update(int id, UpdateReservationDto dto);
    void Remove(int id);
}
