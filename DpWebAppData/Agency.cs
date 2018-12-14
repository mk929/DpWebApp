using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DpWebAppData
{
    public class Agency
    {
        public int Id { get; set; }
        public string SystemId { get; set; }
        public int AgencyType { get; set; } = 1;
        public int GroupId { get; set; } = 1;
        public string CompanyName { get; set; } = String.Empty;
        public string ContactPerson { get; set; } = String.Empty;
        public string Address { get; set; } = String.Empty;
        public string Address2 { get; set; } = String.Empty;
        public string Phone { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
        public DateTime IntroDate { get; set; } = System.Data.SqlTypes.SqlDateTime.MaxValue.Value;

    }
}
