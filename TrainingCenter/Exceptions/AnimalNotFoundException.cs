namespace TrainingCenter.Exceptions;

public class AnimalNotFoundException(int id) : Exception($"Animal with id {id} not found");