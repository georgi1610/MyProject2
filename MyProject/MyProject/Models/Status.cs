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
        public string StatusName { get; set; }

        public virtual ICollection<Request> Requests { get; set; }

    }

}