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
        [Display(Name = "EMail")]
        [StringLength(100, MinimumLength = 3)]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
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
        public System.DateTime HireDate { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)] 
        [Display(Name = "Position")]
        public string Position { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)] 
        [Display(Name = "Department")]
        public string Department { get; set; }

        public virtual Employee SuperiorEmployee { get; set; }
        //public virtual ICollection<Request> Requests { get; set; }
    
        //[Required]
        //public virtual ApplicationUser AppUser { get; set; }
    }

}