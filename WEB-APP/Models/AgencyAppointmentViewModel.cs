using DpWebAppData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DpWebApp.Models
{
    public class AgencyAppointmentVM
    {
        [Required]
        public int AgentId { get; set; } = 1;

        [Required(ErrorMessage = "Please Select Appointment Date")]
        public DateTime AppointmentDate { get; set; }
        [Required]
        public List<VisaApplication> VisaApplications { get; set; } = new List<VisaApplication>();
    }
}