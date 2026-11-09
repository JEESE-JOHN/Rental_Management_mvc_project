using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc_project.Models
{
    public class rentalapplicationcls
    {
        public int app_id { get; set; }

        public int tenant_id { get; set; }

        public int property_id { get; set; }

        public DateTime application_date { get; set; }

        public string ap_message { get; set; }

        public string document { get; set; }

        public int app_status { get; set; }

        public HttpPostedFileBase file { get; set; }

        public string msg { get; set; }
        public propertycls propertydetails { get; set; }
    }
}