using DpWebAppData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DpWebApp.Models
{
    public class AgencyViewModel
    {
        public AgencyViewModel()
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

            Entity = new Agency();
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
        public List<Agency> Agencies { get; set; } = new List<Agency>();

        // SearchEntity is search parameter passing back from View
        public Agency Entity { get; set; } = new Agency(); 
        public Agency SearchEntity { get; set; } = new Agency();
        public bool IsValid { get; set; }
        public string Mode { get; set; }
        public string EventCommand { get; set; } = string.Empty;

        public bool IsDetailAreaVisible { get; set; }
        public bool IsListAreaVisible { get; set; }
        public bool IsSearchAreaVisible { get; set; }


        public void Get()
        {
            AgencyManagerData mgr = new AgencyManagerData();
            Agencies = mgr.GetAgencies();
        }
        public void Get(Agency Agency)
        {
            AgencyManagerData mgr = new AgencyManagerData();
            Agencies = mgr.GetAgencies(Agency);
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