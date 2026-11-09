using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvc_project.Models;

namespace mvc_project.Controllers
{
    public class admininsertController : Controller
    {
        // GET: admininsert
        mvc_projectEntities dbobj = new mvc_projectEntities();
        public ActionResult insertadmin_pageload()
        {
            return View();
        }
        public ActionResult insertadmin_click(admininsertcls clsobj)
        {
            if (ModelState.IsValid)
            {
                var getmaxid = dbobj.sp_maxidlogin().FirstOrDefault();
                int mid = Convert.ToInt32(getmaxid);
                int regid = 0;

                if (mid == 0)
                {
                    regid = 1;
                }
                else
                {
                    regid = mid + 1;
                }

                dbobj.sp_adminreg(regid, clsobj.name, clsobj.email);
                dbobj.sp_login(regid, clsobj.username, clsobj.pass, "admin");

                clsobj.adminmsg = "Successfully registered";

                return View("insertadmin_pageload", clsobj);
            }

            return View("insertadmin_pageload", clsobj);
        }
    }
}