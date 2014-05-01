using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyProject.Models
{
    public class Country
    {
        public Country()
        {
        }
        [Key]
        public int CountryId { get; set; }
        [Required]
        public string CountryName { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }

    }

}