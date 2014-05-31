using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyProject.Models
{
    public class Allowance
    {   
        public Allowance()
        {
        }
        [Key]
        public int AllowanceId { get; set; }
        
        [Required]
        [Display(Name = "Allowance Type")]
        [StringLength(50, MinimumLength = 3)] 
        public string Type { get; set; }
        
        [Required]
        [Display(Name = "Allowance Amount")] 
        public int Amount { get; set; }
        
        [Required]
        [Display(Name = "Currency")]
        [DataType(DataType.Currency)] 
        
        public string Currency { get; set; }

        public virtual ICollection<Request> Requests { get; set; }
    }
}