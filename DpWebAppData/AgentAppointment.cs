using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DpWebAppData
{
    class AgentAppointment
    {
        public int AgentId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public List<VisaApplication> VisaApplications { get; set; } = new List<VisaApplication>();

    }
}
