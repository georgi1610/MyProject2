using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyProject.Validation
{
    public class MyHireDateValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var hiredate = (DateTime)value;
                if (hiredate > DateTime.Now.AddHours(1))
                {
                    var errormessage = "Hire Date can't be in the future.";
                    return new ValidationResult(errormessage);
                }
            }
            return ValidationResult.Success;
        }
    }

    public class MyDepartureDateValidator : ValidationAttribute
    {
        private string propertyToCompare;
        public MyDepartureDateValidator(string compareWith = "")
        {
            propertyToCompare = compareWith;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var basePropertyInfo = validationContext.ObjectType.GetProperty(propertyToCompare);
            var subDate = (DateTime)basePropertyInfo.GetValue(validationContext.ObjectInstance, null);
            
            if (value != null)
            {
                var depdate = (DateTime)value;
                if (depdate < subDate)
                {
                    var errormessage = "Departure Date must be in the future, after current date.";
                    return new ValidationResult(errormessage);
                }
            }
            return ValidationResult.Success;
        }
    }

    public class MyDepDateValidator : ValidationAttribute
    {
        public MyDepDateValidator()
        {
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var depdate = (DateTime)value;
                if (depdate < DateTime.Now)
                {
                    var errormessage = "Departure Date must be in the future, after current date.";
                    return new ValidationResult(errormessage);
                }
            }
            return ValidationResult.Success;
        }
    }

    public class MyReturnDateValidator : ValidationAttribute
    {
        private string propertyToCompare;
        
        public MyReturnDateValidator(string compareWith = "")
        {
            propertyToCompare = compareWith;
        }
        
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var basePropertyInfo = validationContext.ObjectType.GetProperty(propertyToCompare);
            var depDate = (DateTime)basePropertyInfo.GetValue(validationContext.ObjectInstance, null);
            if (value != null)
            {
                var retDate = (DateTime)value;
                if (retDate < depDate)
                {
                    var errormessage = "Return Date must be after Departure Date.";
                    return new ValidationResult(errormessage);
                }
            }
            return ValidationResult.Success;
        }
    }

    public class MyRetDateValidator : ValidationAttribute
    {
        private string propertyToCompare;

        public MyRetDateValidator(string compareWith = "")
        {
            propertyToCompare = compareWith;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var basePropertyInfo = validationContext.ObjectType.GetProperty(propertyToCompare);
            var depDate = (DateTime)basePropertyInfo.GetValue(validationContext.ObjectInstance, null);
            if (value != null)
            {
                var retDate = (DateTime)value;
                if (retDate < depDate)
                {
                    var errormessage = "Return Date must be after Departure Date.";
                    return new ValidationResult(errormessage);
                }
            }
            return ValidationResult.Success;
        }
    }
}