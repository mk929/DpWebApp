using DpWebAppData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DpWebApp.Models
{
    public class AgentAppointmentVM
    {
        [Required]
        public int AgentId { get; set; } = 1;
        [Required]
        public DateTime AppointmentDate { get; set; }
        [Required]
        public List<VisaApplication> VisaApplications { get; set; } = new List<VisaApplication>();
    }
}