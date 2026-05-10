namespace TrainingCenter.Exceptions;

public class ReservationConflictException(int roomId, DateOnly date)
    : Exception($"A reservation conflict exists for room {roomId} on {date}");
