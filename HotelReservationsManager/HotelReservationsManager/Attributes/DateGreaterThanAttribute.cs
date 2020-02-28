using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HotelReservationsManager.Attributes
{
    //[AttributeUsage(AttributeTargets.Property)]
    //public class DateGreaterThanAttribute : ValidationAttribute
    //{
    //    public DateGreaterThanAttribute(string dateToCompareToFieldName)
    //    {
    //        DateToCompareToFieldName = dateToCompareToFieldName;
    //    }

    //    private string DateToCompareToFieldName { get; set; }

    //    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    //    {
    //        DateTime earlierDate = (DateTime)value;

    //        DateTime laterDate = (DateTime)validationContext.ObjectType.GetProperty(DateToCompareToFieldName).GetValue(validationContext.ObjectInstance, null);

    //        if (laterDate > earlierDate)
    //        {
    //            return ValidationResult.Success;
    //        }
    //        else
    //        {
    //            return new ValidationResult("Date is not later");
    //        }
    //    }
    //}

    public class DateGreaterThan : ValidationAttribute
    {
        private string _startDatePropertyName;
        public DateGreaterThan(string startDatePropertyName)
        {
            _startDatePropertyName = startDatePropertyName;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var propertyInfo = validationContext.ObjectType.GetProperty(_startDatePropertyName);
            if (propertyInfo == null)
            {
                return new ValidationResult(string.Format("Unknown property {0}", _startDatePropertyName));
            }
            var propertyValue = propertyInfo.GetValue(validationContext.ObjectInstance, null);
            if ((DateTime)value > (DateTime)propertyValue)
            {
                return ValidationResult.Success;
            }
            else
            {
                var startDateDisplayName = propertyInfo
                    .GetCustomAttributes(typeof(DisplayAttribute), true)
                    .Cast<DisplayAttribute>()
                    .Single()
                    .Name;
                return new ValidationResult(validationContext.DisplayName + " must be later than " + startDateDisplayName + ".");
            }
        }
    }
}
