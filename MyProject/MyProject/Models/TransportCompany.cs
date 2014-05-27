using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyProject.Models
{
    public class TransportCompany
    {
        public TransportCompany()
        {
        }
        [Key]
        public int TransportCompanyId { get; set; }
        
        [Required]
        [Display(Name="Transport Company")]
        [StringLength(50, MinimumLength = 3)]
        public string CompanyName { get; set; }
    
       // public virtual ICollection<Transport> Transports { get; set; }
    }
}