using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DpApptLib
{
    public class ConsularApptDTO
    {
        public int ID { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public int AppointmentType { get; set; }        
        public int QueueNumber { get; set; }        
        public string Name { get; set; }        
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string PlaceOfBirth { get; set; }        
        public string Nationality { get; set; }        
        public string NRIC_No { get; set; }        
        public string PassportNumber { get; set; }
        public DateTime? PassportIssuedDate { get; set; }
        public string ConsulateLocation { get; set; }
        public string StayPermitNumber { get; set; }        
        public int StayType { get; set; }        
        public string EmployerName { get; set; }
        public string Occupation { get; set; }
        public string ContactAddr1 { get; set; }        
        public string ContactAddr2 { get; set; }
        public string ContactPhone { get; set; }        
        public string ContactEmail { get; set; }
        public string HomeAddr1 { get; set; }
        public string HomeAddr2 { get; set; }        
        public string HomePhone { get; set; }
        public int AppointmentStatus { get; set; }
        public string ActivationCode { get; set; }
        public string Note { get; set; }
    }
}
