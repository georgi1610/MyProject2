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
        [StringLength(50, MinimumLength = 3)]
        public string TransportCompany { get; set; }
    
        public virtual ICollection<Request> Requests { get; set; }
    }
}