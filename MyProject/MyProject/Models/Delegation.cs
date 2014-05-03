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
        [Display(Name = "Delegation")]
        public int DelegationId { get; set; }
        [Required]
        [Display(Name = "Delegation Type")]
        [StringLength(50, MinimumLength = 3)]
        public string DelegationType { get; set; }

        public virtual ICollection<Request> Requests { get; set; }
    }
}
