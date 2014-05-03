using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyProject.Validation
{
    public class MyDepartureDateValidator : ValidationAttribute
    {
        /*
        public static ValidationResult ValidateDate(DateTime date1)//, DateTime date2)
        {
            if (date1.Date < DateTime.Today)
            {
                return new ValidationResult("Departure Date cannot be in the past.");
            }
             if (date2.Date < date1.Date)
             {
                 return new ValidationResult("Return Date cannot be before Departure Date.");
             }

             if (date1.Date.AddDays(30) < date2.Date)
             {
                 return new ValidationResult("Delegation Duration is maximum 30 days.");
             }
             
            return ValidationResult.Success;
        }
        */
      

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var depdate = (DateTime)value;
                if (depdate < DateTime.Now)
                {
                    var errormessage = "Departure Date must be in the future";
                    return new ValidationResult(errormessage);
                }
            }
            return ValidationResult.Success;
        }
    }
}