using System;
using System.ComponentModel.DataAnnotations;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class DateComparisonAttribute : ValidationAttribute
{
    private readonly string otherPropertyName;

    public DateComparisonAttribute(string otherPropertyName)
    {
        this.otherPropertyName = otherPropertyName;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var otherProperty = validationContext.ObjectType.GetProperty(otherPropertyName);

        if (otherProperty == null)
        {
            return new ValidationResult($"Property '{otherPropertyName}' not found.");
        }

        var otherValue = otherProperty.GetValue(validationContext.ObjectInstance, null);
        var currentValue = (DateTime)value;

        if (currentValue > (DateTime)otherValue)
        {
            return new ValidationResult(ErrorMessage ?? $"'{validationContext.DisplayName}' must be before '{otherPropertyName}'.");
        }

        return ValidationResult.Success;
    }
}
