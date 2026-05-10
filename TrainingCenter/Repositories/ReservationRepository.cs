using TrainingCenter.Models;

namespace TrainingCenter.Repositories;

public class ReservationRepository : IReservationRepository
{
    private static int _nextId = 7;
    private readonly List<Reservation> _reservations =
    [
        new() { Id = 1, RoomId = 1, OrganizerName = "Ichigo Kurosaki", Topic = "Wprowadzenie do C#",         Date = new DateOnly(2026, 5, 12), StartTime = new TimeOnly(8,  0), EndTime = new TimeOnly(10, 0), Status = "confirmed" },
        new() { Id = 2, RoomId = 2, OrganizerName = "Rukia Kuchiki",  Topic = "Warsztaty z HTTP i REST",    Date = new DateOnly(2026, 5, 10), StartTime = new TimeOnly(10, 0), EndTime = new TimeOnly(12,30), Status = "confirmed" },
        new() { Id = 3, RoomId = 1, OrganizerName = "Yachiru Unohana", Topic = "Wzorce projektowe",          Date = new DateOnly(2026, 5, 14), StartTime = new TimeOnly(13, 0), EndTime = new TimeOnly(15, 0), Status = "planned"   },
        new() { Id = 4, RoomId = 3, OrganizerName = "Kenpachi Zaraki", Topic = "Testowanie jednostkowe",     Date = new DateOnly(2026, 5, 11), StartTime = new TimeOnly(9,  0), EndTime = new TimeOnly(11, 0), Status = "planned"   },
        new() { Id = 5, RoomId = 4, OrganizerName = "Uryu Ishida",    Topic = "Bazy danych SQL",            Date = new DateOnly(2026, 5, 13), StartTime = new TimeOnly(14, 0), EndTime = new TimeOnly(16, 0), Status = "confirmed" },
        new() { Id = 6, RoomId = 2, OrganizerName = "Byakuya Kuchiki",Topic = "ASP.NET Core MVC",           Date = new DateOnly(2026, 5, 15), StartTime = new TimeOnly(11, 0), EndTime = new TimeOnly(13, 0), Status = "cancelled" },
    ];

    public IEnumerable<Reservation> GetAll() => _reservations;

    public IEnumerable<Reservation> GetFiltered(DateOnly? date, string? status, int? roomId)
    {
        var query = _reservations.AsEnumerable();
        if (date.HasValue)
            query = query.Where(r => r.Date == date.Value);
        if (!string.IsNullOrEmpty(status))
            query = query.Where(r => r.Status.Equals(status, StringComparison.OrdinalIgnoreCase));
        if (roomId.HasValue)
            query = query.Where(r => r.RoomId == roomId.Value);
        return query;
    }

    public Reservation? GetById(int id) => _reservations.FirstOrDefault(r => r.Id == id);

    public void Add(Reservation reservation)
    {
        reservation.Id = _nextId++;
        _reservations.Add(reservation);
    }

    public bool Update(Reservation reservation)
    {
        var existing = GetById(reservation.Id);
        if (existing is null) return false;

        existing.RoomId = reservation.RoomId;
        existing.OrganizerName = reservation.OrganizerName;
        existing.Topic = reservation.Topic;
        existing.Date = reservation.Date;
        existing.StartTime = reservation.StartTime;
        existing.EndTime = reservation.EndTime;
        existing.Status = reservation.Status;
        return true;
    }

    public void Remove(Reservation reservation) => _reservations.Remove(reservation);

    public bool HasConflict(int roomId, DateOnly date, TimeOnly startTime, TimeOnly endTime, int? excludeId = null)
    {
        return _reservations.Any(r =>
            r.RoomId == roomId &&
            r.Date == date &&
            r.Status != "cancelled" &&
            (excludeId is null || r.Id != excludeId) &&
            r.StartTime < endTime &&
            r.EndTime > startTime);
    }

    public IEnumerable<Reservation> GetFutureByRoom(int roomId)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        return _reservations.Where(r => r.RoomId == roomId && r.Date >= today);
    }
}
