using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyProject.Models
{
    public class Status
    {
        public Status()
        { 
        }
        [Key]
        public int StatusId { get; set; }
        [Required]
        [Display(Name = "Status Name")]
        [StringLength(50, MinimumLength = 3)]
        public string StatusName { get; set; }

        public virtual ICollection<Request> Requests { get; set; }

    }

}