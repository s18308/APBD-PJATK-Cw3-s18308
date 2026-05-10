using TrainingCenter.Models;

namespace TrainingCenter.Repositories;

public interface IReservationRepository
{
    IEnumerable<Reservation> GetAll();
    IEnumerable<Reservation> GetFiltered(DateOnly? date, string? status, int? roomId);
    Reservation? GetById(int id);
    void Add(Reservation reservation);
    bool Update(Reservation reservation);
    void Remove(Reservation reservation);
    bool HasConflict(int roomId, DateOnly date, TimeOnly startTime, TimeOnly endTime, int? excludeId = null);
    IEnumerable<Reservation> GetFutureByRoom(int roomId);
}
