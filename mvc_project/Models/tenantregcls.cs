using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mvc_project.Models
{
    public class tenantregcls
    {
        public int tenant_id { get; set; }

        [Required(ErrorMessage = "enter the name")]
        public string name { get; set; }

        [Required(ErrorMessage = "enter the age")]
        [Range(18, 100, ErrorMessage = "enter valid age")]
        public int age { get; set; }

        [Required(ErrorMessage = "enter the email")]
        [EmailAddress(ErrorMessage = "enter valid email id")]
        public string email { get; set; }

        [Required(ErrorMessage = "enter the phone")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "enter valid number")]
        public string phone { get; set; }

        [Required(ErrorMessage = "enter the address")]
        public string address { get; set; }

        [Required(ErrorMessage = "enter the occupation")]
        public string occupation { get; set; }

        public string status { get; set; }
        [Required(ErrorMessage = "enter the username")]
        public string username { get; set; }

        public string pass { get; set; }

        [Compare("pass", ErrorMessage = "Password mismatch")]
        public string cpassword { get; set; }

        public string tenantmsg { get; set; }
    }
}