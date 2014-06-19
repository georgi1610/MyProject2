using MyProject.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyProject.Models
{
    public class Employee
    {
        public Employee()
        {
        }
        [Key]
        public int EmployeeId { get; set; }
        
        [Required]
        [Display(Name = "First Name")]
        [StringLength(50, MinimumLength = 3)] 
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        [StringLength(50, MinimumLength = 3)] 
        public string MiddleName { get; set; }
        
        [Required]
        [Display(Name = "Last Name")]
        [StringLength(50, MinimumLength = 3)] 
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        [StringLength(150, MinimumLength = 6)]
        public string FullName
        {
            get
            {
                return FirstName + " " + MiddleName + " " + LastName;
            }
        }

        [Required]
        [Display(Name = "EMail")]
        [StringLength(100, MinimumLength = 3)]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})\s*"
            , ErrorMessage = "The email address is invalid.")]
        public string EMail { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string EMailPassword { get; set; }

        [Required]
        [Display(Name = "Personal ID")]
        [StringLength(50, MinimumLength = 10)] 
        public string PersonalID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name="Hire Date")]
        [MyHireDateValidator()]
        public DateTime HireDate { get; set; }
       
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Required]
        [Display(Name = "Headquarter")]
        public Address Headquarter { get; set; }

        public int Headquarter_Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)] 
        [Display(Name = "Position")]
        public string Position { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)] 
        [Display(Name = "Department")]
        public string Department { get; set; }

        [Display(Name = "Superior")]
        public virtual Employee SuperiorEmployee { get; set; }
     
    }

}