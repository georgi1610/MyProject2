using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace MyProject.Models
{
    public class Delegation
    {
        public Delegation()
        {
        }
        [Key]
        public int DelegationId { get; set; }
        [Required]
        public string DelegationType { get; set; }

        public virtual ICollection<Request> Requests { get; set; }
    }
}
