using System.ComponentModel.DataAnnotations;

public class StartTimeNotLaterThanEndTimeAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var shift = (Shift)validationContext.ObjectInstance;
        if (shift.StartTime >= shift.EndTime)
        {
            return new ValidationResult("Zmiana nie może zaczynas się wcześniej niż się kończy.");
        }

        return ValidationResult.Success;
    }
}