using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mvc_project.Models
{
    public class ownerregcls
    {
        [Required(ErrorMessage = "enter the name")]
        public string own_name { get; set; }

        [Required(ErrorMessage = "enter the email")]
        [EmailAddress(ErrorMessage = "enter valid email id")]
        public string own_email { get; set; }

        [Required(ErrorMessage = "enter the address")]
        public string own_address { get; set; }

        [Required(ErrorMessage = "enter the phone")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "enter valid number")]
        public string own_phone { get; set; }

        public string own_status { get; set; }

        public string username { get; set; }

        public string pass { get; set; }

        [Compare("pass", ErrorMessage = "Password mismatch")]
        public string cpassword { get; set; }

        public string ownermsg { get; set; }
    }
}