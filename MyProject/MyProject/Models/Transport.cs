using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyProject.Models
{
    public class Transport
    {
        public Transport()
        {
        }
        [Key]
        public int TransportId { get; set; }

        [Required]
        [Display(Name="Transport Company")]
        public string TransportCompany { get; set; }
    
        public virtual ICollection<Request> Requests { get; set; }
    }
}