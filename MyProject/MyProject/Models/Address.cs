using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace MyProject.Models
{
    public class Address
    {
        public Address()
        {
        }

        [Key]
        public int AddressId { get; set; }

        [Required]
        [Display(Name = "Company")]
        [StringLength(50, MinimumLength = 3)]
        public string CompanyName { get; set; }
        
        [Required]
        [Display(Name = "Street")]
        [StringLength(50, MinimumLength = 3)]
        public string StreetName { get; set; }
        
        [Required]
        [Display(Name = "Number")]
        [Range(0, short.MaxValue)]
        public short Number { get; set; }
        
        [Required]
        [Display(Name = "Postal Code")]
        [StringLength(50, MinimumLength = 6)]
        public string PostalCode { get; set; }
       
        [Required]
        public int CountryId { get; set; }

       // [Required]
        public virtual Country Country { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        //public virtual Employee employee { get; set; }
        //[Required]
     //   public virtual Transport DepartureTransport { get; set; }

 //       [Required]
       // public virtual Transport ArrivalTransport { get; set; }

       
    }
}
