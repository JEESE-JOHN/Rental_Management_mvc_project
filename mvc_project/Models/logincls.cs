using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mvc_project.Models
{
    public class logincls
    {
        [Required(ErrorMessage = "Enter username")]
        public string uname { get; set; }
        [Required(ErrorMessage = "Enter password")]
        public string pw { get; set; }
        public string msg { get; set; }

    }
}