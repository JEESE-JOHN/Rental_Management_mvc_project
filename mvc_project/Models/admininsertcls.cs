using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mvc_project.Models
{
    public class admininsertcls
    {


        [Required(ErrorMessage = "enter the name")]
        public string name { get; set; }

        [Required(ErrorMessage = "enter the email")]
        [EmailAddress(ErrorMessage = "enter valid email id")]
        public string email { get; set; }
        public string username { set; get; }

        public string pass { set; get; }
        [Compare("pass", ErrorMessage = "Password mismatch")]
        public string cpassword { set; get; }
        public string adminmsg { set; get; }
    }
}