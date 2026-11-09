using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc_project.Models
{
    public class searchpropertycls
    {
        public int property_id { get; set; }

        public int owner_id { get; set; }

        public string property_title { get; set; }

        public string description { get; set; }

        public string location { get; set; }

        public decimal? rent { get; set; }

        public int? bedrooms { get; set; }

        public int? bathrooms { get; set; }

        public int? area { get; set; }

        public string photo1 { get; set; }

        public string photo2 { get; set; }

        public DateTime? available_from { get; set; }

        public string lease_duration { get; set; }

        public string property_status { get; set; }

        public string msg { get; set; }
        public List<searchpropertycls> selectproperty { get; set; }

        public searchpropertycls()
        {
            selectproperty = new List<searchpropertycls>();
        }
    }
}