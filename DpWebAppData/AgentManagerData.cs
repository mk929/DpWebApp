using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DpWebAppData
{
    public class AgentManagerData
    {
        public List<Agent> GetAgents()
        {
            return CreateMockData();
        }
        public List<Agent> GetAgents(Agent agent)
        {
            List<Agent> retAgents = new List<Agent>();
            if ( !string.IsNullOrEmpty(agent.AgentName))
            {
                retAgents = CreateMockData().FindAll(a => a.AgentName.ToLower().StartsWith(agent.AgentName));
            }
            return retAgents;
        }
        private List<Agent> CreateMockData()
        {
            List<Agent> agents = new List<Agent>();
            agents.Add(new Agent() { AgentId = 1, AgentName = "ABC Worker's Association", Email = "abc@gmail.com", Phone = "95 887 33883", IntroDate = Convert.ToDateTime("2017-3-18") });
            agents.Add(new Agent() { AgentId = 2, AgentName = "Happy Malaysia", Email = "happy@gmail.com", Phone = "95 112 323", IntroDate = Convert.ToDateTime("2017-1-12") });
            return agents;

        }

    }
}
