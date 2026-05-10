using System.ComponentModel.DataAnnotations;

namespace TrainingCenter.Validation;

public class EndTimeAfterStartTimeAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        TimeOnly? endTime = value as TimeOnly?;

        var startTimeProp = validationContext.ObjectType.GetProperty("StartTime");
        if (startTimeProp is null)
            return ValidationResult.Success;

        var startTime = (TimeOnly?)startTimeProp.GetValue(validationContext.ObjectInstance);

        if (endTime.HasValue && startTime.HasValue && endTime <= startTime)
        {
            return new ValidationResult("EndTime must be later than StartTime.");
        }

        return ValidationResult.Success;
    }
}
