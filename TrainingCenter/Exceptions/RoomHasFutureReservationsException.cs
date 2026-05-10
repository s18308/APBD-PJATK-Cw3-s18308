namespace TrainingCenter.Exceptions;

public class RoomHasFutureReservationsException(int roomId)
    : Exception($"Room with id {roomId} has future reservations and cannot be deleted");
