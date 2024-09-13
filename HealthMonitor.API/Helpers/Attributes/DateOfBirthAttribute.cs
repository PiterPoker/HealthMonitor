using System.ComponentModel.DataAnnotations;

namespace HealthMonitor.API.Helpers.Attributes
{
    public class DateOfBirthAttribute : ValidationAttribute
    {
        public DateOfBirthAttribute()
        {
            ErrorMessage = "Date of birth must be in the past";
        }

        public override bool IsValid(object value)
        {
            if (value is DateTime date)
            {
                return date < DateTime.Now;
            }
            return false;
        }
    }
}
