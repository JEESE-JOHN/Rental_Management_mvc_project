using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc_project.Models
{
    public class ownerapplicationcls
    {
        public int app_id { get; set; }
        public int tenant_id { get; set; }
        public string tenant_name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string occupation { get; set; }
        public int property_id { get; set; }
        public string property_title { get; set; }
        public DateTime application_date { get; set; }
        public string ap_message { get; set; }
        public string document { get; set; }
        public int app_status { get; set; }
    }
}