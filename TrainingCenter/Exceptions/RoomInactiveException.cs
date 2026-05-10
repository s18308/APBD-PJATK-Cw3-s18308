namespace TrainingCenter.Exceptions;

public class RoomInactiveException(int roomId)
    : Exception($"Room with id {roomId} is not active and cannot be reserved");
