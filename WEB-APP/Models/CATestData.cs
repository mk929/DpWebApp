using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DpWebApp.Models
{
    public class CATestData
    {
        private static int _i = 0;
        public static ConsularApptVM GetNewAppt(int requesterType)
        {
            _i++;
            Random r = new Random();
            DateTime appointmentDate = DateTime.Today;
            do
            {
                appointmentDate = (DateTime.Now.Hour > 12) ?        // afternoon? 
                        DateTime.Today.AddDays(r.Next(1, 15))       // then next 14 days from tomorrow
                        : DateTime.Today.AddDays(r.Next(0, 14));    // else next 14 days from today

            } while (appointmentDate.DayOfWeek == DayOfWeek.Saturday 
                    || appointmentDate.DayOfWeek == DayOfWeek.Sunday);

            requesterType = ((requesterType == 1 || requesterType == 2) ? requesterType : r.Next(1, 3));

            if ( requesterType == 1) // Agency
            {
                return new ConsularApptVM()
                {
                    RequesterType = requesterType,
                    AppointmentDate = appointmentDate,
                    AppointmentType = r.Next(1, 3),
                    Name = String.Format("Agency_{0}", _i),
                    ContactAddr1 = String.Format("[{0}] Agency Address Line 1", _i),
                    ContactAddr2 = String.Format("[{0}] Agency Address Line 2", _i),
                    ContactPhone = String.Format("+65 {0}",
                                        String.Format("{0}", r.Next(1000000, 9999999).ToString("D8"))),
                    ContactEmail = String.Format("{0}@test.com", String.Format("User_{0}", _i)),
                    AppointmentStatus = 0,
                    ActivationCode = "",
                    Note = "TEST"
                    // Note = String.Format("Sumit request at: {0:dd MMM, yyyy [hh:mm:ss]}", DateTime.Now)
                };

            }
            

            return new ConsularApptVM()
            {
                RequesterType = requesterType,
                AppointmentDate = appointmentDate,
                AppointmentType = r.Next(1, 2),
                Name = String.Format("User_{0}", _i),
                Gender = r.Next(1, 2) == 1 ? "M" : "F",
                DateOfBirth = DateTime.Today.AddYears(r.Next(-60, -15)).AddDays(r.Next(1,365)),
                PlaceOfBirth = String.Format("City/Town Name: {0}, Myanmar", _i),
                Nationality = "MM",
                NRIC_No = String.Format("NRIC{0}", r.Next(1000000, 9999999).ToString("D8")),
                PassportNumber = String.Format("{0}", r.Next(1, 9999999).ToString("D8")),
                PassportIssuedDate = DateTime.Today.AddYears(r.Next(-20, -1)).AddDays(r.Next(1, 365)),
                ContactAddr1 = String.Format("[{0}] Singapore Address Line 1", _i),
                ContactAddr2 = String.Format("[{0}] Singapore Address Line 2", _i),
                ContactPhone = String.Format("+65 {0}", 
                                    String.Format("{0}", r.Next(1000000, 9999999).ToString("D8"))),
                ContactEmail = String.Format("{0}@test.com", String.Format("User_{0}", _i)),
                AppointmentStatus = 0,
                ActivationCode = "",
                Note = "TEST"
                // Note = String.Format("Sumit request at: {0:dd MMM, yyyy [hh:mm:ss]}", DateTime.Now)
            };
        }
    }
}