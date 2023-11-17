using System.ComponentModel.DataAnnotations;

namespace DigiCV.Web.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly long _maxFileSize; // Size in bytes

        public MaxFileSizeAttribute(long maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                if (file.Length > _maxFileSize)
                {
                    return new ValidationResult($"The file size must not exceed {_maxFileSize / 1024} KB.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
