using System.ComponentModel.DataAnnotations;
using TrainingCenter.Validation;

namespace TrainingCenter.DTOs;

public class UpdateReservationDto
{
    public int RoomId { get; set; }

    [Required, MaxLength(100)]
    public string OrganizerName { get; set; } = string.Empty;

    [Required, MaxLength(200)]
    public string Topic { get; set; } = string.Empty;

    public DateOnly Date { get; set; }

    public TimeOnly StartTime { get; set; }

    [EndTimeAfterStartTime]
    public TimeOnly EndTime { get; set; }

    [Required]
    public string Status { get; set; } = "planned";
}
