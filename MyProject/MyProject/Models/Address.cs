using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public string CompanyName { get; set; }
        [Required]
        public string StreetName { get; set; }
        [Required]
        public short Number { get; set; }
        [Required]
        public string PostalCode { get; set; }
        [Required]
        public int CountryId { get; set; }

        //public int RequestId { get; set; }
        
        public virtual Country Country { get; set; }

        //public virtual ICollection<Request> Requests { get; set; }


    }
}
