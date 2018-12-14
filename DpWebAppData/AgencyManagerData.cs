using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DpWebAppData
{
    public class AgencyManagerData
    {
        public List<Agency> GetAgencies()
        {
            return CreateMockData();
        }
        public List<Agency> GetAgencies(Agency agency)
        {
            List<Agency> retVal = new List<Agency>();
            if ( !string.IsNullOrEmpty(agency.CompanyName))
            {
                retVal = CreateMockData().FindAll(a => a.CompanyName.ToLower().StartsWith(agency.CompanyName));
            }
            return retVal;
        }
        private List<Agency> CreateMockData()
        {
            List<Agency> agencies = new List<Agency>();
            agencies.Add(new Agency() { Id = 1, CompanyName = "ABC Worker's Association", Email = "abc@gmail.com", Phone = "95 887 33883", IntroDate = Convert.ToDateTime("2017-3-18") });
            agencies.Add(new Agency() { Id = 2, CompanyName = "Happy Malaysia", Email = "happy@gmail.com", Phone = "95 112 323", IntroDate = Convert.ToDateTime("2017-1-12") });
            return agencies;

        }

    }
}
