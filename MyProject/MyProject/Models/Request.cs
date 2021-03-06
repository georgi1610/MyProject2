﻿using MyProject.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace MyProject.Models
{
    public class Request
    {
        [Key]
        public int RequestId { get; set; }

        [DataType(DataType.Date)]
        [Display(Name="Submit Date")]
        public DateTime SubmitDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Departure Date")]
        [MyDepartureDateValidator(compareWith: "SubmitDate")]
        public DateTime DepartureDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Return Date")]
        [MyReturnDateValidator(compareWith: "DepartureDate")]
        public DateTime ReturnDate { get; set; }

        [Required]
        [Display(Name = "Status")]
        public int StatusId { get; set; }

        [Display(Name = "Motivation")]
        [StringLength(50, MinimumLength = 3)] 
        public string Motivation { get; set; }

        //[Required]
        [Display(Name = "Description")]
        [StringLength(50, MinimumLength = 3)] 
        public string Description { get; set; }

        [Required]
        [Display(Name = "Delegation Type")]
        public int DelegationId { get; set; }
        
        [Required]
        [Display(Name = "Departure Address req")]
        public int DepartureAddressId { get; set; }

        [Required]
        [Display(Name = "Return Address req")]
        public int ReturnAddressId { get; set; }
        
        [Display(Name = "Transport")]
        public int? TransportId { get; set; }

        [Display(Name = "Allowance")]
        public int? AllowanceId { get; set; }

        public virtual Delegation Delegation { get; set; }
        public virtual Status Status { get; set; }

        public virtual Address DepartureAddress { get; set; }
        public virtual Address ReturnAddress { get; set; }

        public virtual Allowance Allowance { get; set; }
        public virtual Transport Transport { get; set; }

        public virtual Employee Applicant { get; set; }
        public virtual Employee Approver { get; set; }
        public virtual Employee HREmployee { get; set; }
        public virtual Employee StandIn { get; set; }

    }
}
