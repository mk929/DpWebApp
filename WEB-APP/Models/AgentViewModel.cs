using DpWebAppData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DpWebApp.Models
{
    public class AgentViewModel
    {
        public AgentViewModel()
        {
            Init();
        }

        private void Init()
        {
            EventCommand = "list";
            ListMode();
        }
        private void ListMode()
        {
            Mode = "list";
            IsValid = true;

            IsSearchAreaVisible = true;
            IsListAreaVisible = true;
            IsDetailAreaVisible = false;
        }
        private void Add()
        {
            IsValid = true;

            Entity = new Agent();
            Entity.IntroDate = DateTime.Now;

            AddMode();
        }
        private void AddMode()
        {
            Mode = "add";

            IsListAreaVisible = false;
            IsSearchAreaVisible = false;
            IsDetailAreaVisible = true;
        }
        public List<Agent> Agents { get; set; } = new List<Agent>();

        // SearchEntity is search parameter passing back from View
        public Agent Entity { get; set; } = new Agent(); 
        public Agent SearchEntity { get; set; } = new Agent();
        public bool IsValid { get; set; }
        public string Mode { get; set; }
        public string EventCommand { get; set; } = string.Empty;

        public bool IsDetailAreaVisible { get; set; }
        public bool IsListAreaVisible { get; set; }
        public bool IsSearchAreaVisible { get; set; }


        public void Get()
        {
            AgentManagerData mgr = new AgentManagerData();
            Agents = mgr.GetAgents();
        }
        public void Get(Agent agent)
        {
            AgentManagerData mgr = new AgentManagerData();
            Agents = mgr.GetAgents(agent);
        }

        public void HandleRequest()
        {
            switch (EventCommand.ToLower())
            {
                case "list":
                    Get();
                    break;
                case "search":
                    Get();
                    break;
                case "resetsearch":
                    break;
                case "add":
                    Add();
                    break;
                case "save":
                    ListMode();
                    Get();
                    break;
                case "cancel":
                    ListMode();
                    Get();
                    break;
                default:
                    break;
            }
        }
    }
}