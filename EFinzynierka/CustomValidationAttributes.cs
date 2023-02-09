using System.ComponentModel.DataAnnotations;

public class StartTimeNotLaterThanEndTimeAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var shift = (Shift)validationContext.ObjectInstance;
        if (shift.StartTime >= shift.EndTime)
        {
            return new ValidationResult("The start time must be earlier than the end time.");
        }

        return ValidationResult.Success;
    }
}