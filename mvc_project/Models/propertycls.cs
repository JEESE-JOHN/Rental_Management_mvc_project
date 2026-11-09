using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace mvc_project.Models
{
    public class propertycls
    {
        public int property_id { get; set; }
        public int owner_id { get; set; }

        [Required(ErrorMessage = "Enter property title")]
        public string property_title { get; set; }

        [Required(ErrorMessage = "Enter description")]
        public string description { get; set; }

        [Required(ErrorMessage = "Enter location")]
        public string location { get; set; }

        [Required(ErrorMessage = "Enter rent")]
        public decimal rent { get; set; }

        [Required(ErrorMessage = "Enter number of bedrooms")]
        public int bedrooms { get; set; }

        [Required(ErrorMessage = "Enter number of bathrooms")]
        public int bathrooms { get; set; }

        [Required(ErrorMessage = "Enter area")]
        public int area { get; set; }

        public string photo1 { get; set; }
        public string photo2 { get; set; }

        

        [Required(ErrorMessage = "Select available date")]
        [DataType(DataType.Date)]
        public DateTime available_from { get; set; }

        public string lease_duration { get; set; }

        public string property_status { get; set; }

        public string msg { get; set; }
    }
}