using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HuynhHoangTram.DataObject
{
    public class ProjectData
    {
        public string ProjectName { get; set; } = "";
        public string ProjectType { get; set; } = "";
        public string ProjectStatus { get; set; } = "";
        public string StartDate { get; set; } = "";
        public string EndDate { get; set; } = "";
        public string SizeDay { get; set; } = "";
        public string Location { get; set; } = "";
        public string ProjectManager { get; set; } = "";
        public string DeliveryManager { get; set; } = "";
        public string EngagementManager { get; set; } = "";
        public string ShortDescription { get; set; } = "";
        public string LongDescription { get; set; } = "";
        public string Technologies { get; set; } = "";
        public string ClientName { get; set; } = "";
        public string ClientDescription { get; set; } = "";
        public string ClientIndustry { get; set; } = "";
    }
}