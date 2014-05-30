using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Departure Date")]
        public DateTime DepartureDateTime { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Departure Time")]
        public DateTime DepartureTime { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Arrival Date")]
        public DateTime ArrivalDateTime { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Arrival Time")]
        public DateTime ArrivalTime { get; set; }
 
  //      [Required]
       // public int DepartureAddressId { get; set; }
        
        [Required]
        [Display(Name = "Full Departure Address")]
        public string DepartureAddress { get; set; }
        
        //[Required]
       // public int ArrivalAddressId { get; set; }
        
        [Required]
        [Display(Name = "Full Arrival Address")]
        public string ArrivalAddress { get; set; }
        
        [Display(Name="Transport Company")]
        public virtual TransportCompany TransportComp { get; set; }
        public int TransportCompId { get; set; }

        [Display(Name = "Plane Ticket")]
        public string PlaneTicketPath { get; set; }

        public int DriverId { get; set; }

        public virtual Employee Driver { get; set; }
    
        public virtual ICollection<Request> Requests { get; set; }

    }
}